# -- *Welcome to Moogle!* --

> Blazingly fast

!["My pic was here, I swear :("](./assets/Moogle.png "Best Search Engine")

> Simple C# Search Engine
>
> Built upon .NET 8
>
> Graphical UI built with React

Original work built with Blazor .NET 7
[here](https://github.com/ARKye03/Moogle_Search_Engine.git).

## Features

- Supports searching for various topics.
- Dark Mode and Light Mode.
- Relatively fast, tested with 30 documents (~40mb).
- Ability to use Inclusion operators ('^'), Exclusion operators ('!'), and Proximity operators ('~').
- Possibility to return suggestions once the query is processed and determined to be incorrect or nonexistent in the Corpus.
- Displays small sections of the documents where the requested content was found.
- Displays the Score assigned to each document based on the query.

## How to

Open a terminal and execute:

```shell
git clone https://github.com/ARKye03/Moogle.git
```

```shell
cd Moogle/frontend && npm start
```

## Requirements

- DotNet 8 sdk
- Git
- Nodejs v20+

## Note

- Currently supports only *.txt in ./src/content folder
- WIP.
- Current "Search Engine" was impproved in https://github.com/ARKye03/TauriRoogle
