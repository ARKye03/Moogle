import { Routes, Route } from "react-router-dom";
import "./App.css";
import TopBar from "./components/TopBar";
import SearchInput from "./components/SI";
import SearchResults from "./SearchResults";

function App() {
  return (
    <section>
      <TopBar />
      <div className="mt-16">
        <Routes>
          <Route path="/" element={<SearchInput />} />
          <Route path="/search" element={<SearchResults />} />
        </Routes>
      </div>
    </section>
  );
}

export default App;
