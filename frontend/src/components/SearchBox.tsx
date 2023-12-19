import { useState } from "react";
import { useNavigate } from "react-router-dom";

const SearchBox = () => {
  const [inputText, setInputText] = useState("");
  const navigate = useNavigate();

  const handleSearch = () => {
    if (inputText === "") {
      return;
    } else {
      navigate(`/search?query=${inputText}`);
    }
  };

  return (
    <form onSubmit={(e) => e.preventDefault()}>
      <div className="relative">
        <input
          className="h-20 
            transition-all duration-200 
            bg-[#242731] hover:bg-slate-600 
            text-xl rounded-2xl pl-5 
            lg:w-[800px] sm:w-[575px] w-96 
            outline-none pr-24" // added padding right to avoid overlap with svg
          placeholder="Search with moogle..."
          value={inputText}
          onChange={(e) => setInputText(e.target.value)}
        ></input>
        <button
          className="absolute right-0 cursor-pointer hover:bg-gradient-to-t from-[#2563eb] to-green-500 p-5 rounded-r-2xl"
          onClick={handleSearch}
        >
          <svg
            className="transition-transform duration-300 hover:rotate-[450deg]"
            width="40"
            height="40"
            viewBox="0 0 24 24"
            stroke-width="2"
            stroke="currentColor"
            fill="none"
            stroke-linecap="round"
            stroke-linejoin="round"
          >
            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
            <path d="M10 10m-7 0a7 7 0 1 0 14 0a7 7 0 1 0 -14 0" />
            <path d="M21 21l-6 -6" />
          </svg>
        </button>
      </div>
    </form>
  );
};
export default SearchBox;
