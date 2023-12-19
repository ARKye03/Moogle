import { useLocation } from "react-router-dom";
import { useState } from "react";
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

  fetch(`http://localhost:5152/api/search?query=${query}`)
    .then((response) => {
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return response.json();
    })
    .then((data: SearchResult) => setSearchResult(data))
    .catch((error) => console.error("Fetch error:", error));

  return (
    <div>
      <div className="absolute top-16 left-0">
        <SearchBox />
      </div>
      <h1>Search Results for "{query}"</h1>
      {/* Display the search results here */}
      {searchResult &&
        searchResult.items.map((item) => (
          <div key={item.title}>
            <h2>{item.title}</h2>
            <p>{item.snippet}</p>
          </div>
        ))}
    </div>
  );
}

export default SearchResults;
