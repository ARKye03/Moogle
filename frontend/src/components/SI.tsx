function SearchInput() {
  return (
    <div className="gap-4">
      <div className="flex justify-center mb-10">
        <h1
          className="Moogt effect-underline cursor-default"
          data-text="Moogle!"
        >
          Moogle!
        </h1>
      </div>
      <form>
        <input
          className="h-20 transition duration-200 bg-[#242731] hover:bg-slate-600 text-xl rounded-2xl pl-5 lg:w-[800px] sm:w-[575px] w-96 outline-none"
          placeholder="Search with moogle..."
        ></input>
      </form>
    </div>
  );
}

export default SearchInput;
