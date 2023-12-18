function TopBar() {
  return (
    <div className="fixed top-0 left-0 h-16 w-full bg-[#222531] p-2">
      <nav className="flex flex-row justify-end gap-6">
        <a href="/home">
          <svg
            className="stroke-blue-800 hover:stroke-blue-600 transition duration-200"
            width="42"
            height="42"
            viewBox="0 0 24 24"
            stroke-width="2"
            stroke="currentColor"
            fill="none"
            stroke-linecap="round"
            stroke-linejoin="round"
          >
            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
            <path d="M5 12l-2 0l9 -9l9 9l-2 0" />
            <path d="M5 12v7a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-7" />
            <path d="M9 21v-6a2 2 0 0 1 2 -2h2a2 2 0 0 1 2 2v6" />
          </svg>
        </a>
        <a href="https://github.com/ARKye03/Moogle_Search_Engine">
          <svg
            className="stroke-blue-800 hover:stroke-blue-600 transition duration-200"
            width="42"
            height="42"
            viewBox="0 0 24 24"
            stroke-width="2"
            stroke="currentColor"
            fill="none"
            stroke-linecap="round"
            stroke-linejoin="round"
          >
            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
            <path d="M6 18m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" />
            <path d="M6 6m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" />
            <path d="M18 18m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" />
            <path d="M6 8l0 8" />
            <path d="M11 6h5a2 2 0 0 1 2 2v8" />
            <path d="M14 9l-3 -3l3 -3" />
          </svg>
        </a>
      </nav>
    </div>
  );
}
export default TopBar;
