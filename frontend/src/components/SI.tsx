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
        <textarea
          className="transition duration-200 bg-[#242731] hover:bg-slate-600 text-xl resize-none rounded-2xl pt-6 pl-5 lg:w-[800px] sm:w-[517px] w-96 outline-none"
          placeholder="What do you want?"
        ></textarea>
      </form>
    </div>
  );
}

export default SearchInput;
