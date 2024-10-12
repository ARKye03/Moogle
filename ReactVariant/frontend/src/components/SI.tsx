import SearchBox from "./SearchBox";

function SearchInput() {
  return (
    <div className="gap-4 relative flex flex-col items-center">
      <div className="pl-2 pr-3 flex justify-center mb-10 shadow-xl w-fit transition-transform duration-300 sm:scale-100 scale-75 ">
        <img src="/MoogleLogo.jpeg" alt="Moogle Logo" className="w-36 h-36" />
        <h1
          className="Moogt effect-underline cursor-default transition-all duration-200"
          data-text="Moogle!"
        >
          <span className="transition duration-200 hover:text-green-600">
            M
          </span>
          <span className="transition duration-200 hover:text-red-600">o</span>
          <span className="transition duration-200 hover:text-blue-600">o</span>
          <span className="transition duration-200 hover:text-yellow-400">
            g
          </span>
          <span className="transition duration-200 hover:text-violet-600">
            l
          </span>
          <span className="transition duration-200 hover:text-orange-500">
            e
          </span>
          <span className="transition duration-200 hover:text-pink-500">!</span>
        </h1>
      </div>
      <SearchBox />
    </div>
  );
}
export default SearchInput;
