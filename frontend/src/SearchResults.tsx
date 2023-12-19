import { useLocation } from "react-router-dom";
import { useEffect, useState } from "react";
import SearchBox from "./components/SearchBox";

function useQuery() {
  return new URLSearchParams(useLocation().search);
}

interface SearchItem {
  title: string;
  snippet: string;
  score: number;
}

interface SearchResult {
  items: SearchItem[];
  suggestion: string;
  count: number;
}

function SearchResults() {
  let query = useQuery().get("query");
  const [searchResult, setSearchResult] = useState<SearchResult | null>(null);

  useEffect(() => {
    fetch(`http://localhost:5106/Hello?expression=${query}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then((data: SearchResult) => {
        console.log(data); // Add this line
        setSearchResult(data);
      })
      .catch((error) => console.error("Fetch error:", error));
  }, [query]); // Add query as a dependency

  return (
    <div className="flex flex-col">
      <div className="absolute top-16 left-0">
        <SearchBox />
        <h1>Search Results for "{query}"...</h1>
      </div>
      <ul className="flex flex-col max-h-[700px] mt-20 overflow-auto relative xl:w-[900px] sm:w-[500px] md:w-[700px] lg:w-[800px]">
        {searchResult &&
          searchResult.items &&
          searchResult.items.map((item) => (
            <li key={item.title} className="mb-6">
              <div className="text-left">
                {" "}
                <div className="raise">
                  <p className="underline">{item.title}</p>
                  <p>--{item.snippet} ..</p>
                  <p>--{item.score} ...</p>
                </div>
              </div>
            </li>
          ))}
      </ul>
    </div>
  );
}

export default SearchResults;
