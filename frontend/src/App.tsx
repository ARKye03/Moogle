import "./App.css";
import TopBar from "./components/TopBar";
import SearchInput from "./components/SI";

function App() {
  return (
    <section>
      <TopBar />
      <div className="mt-16">
        <SearchInput />
      </div>
    </section>
  );
}

export default App;
