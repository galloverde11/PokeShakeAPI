# PokeShakeAPI

PokeShakeAPI is an API library to get "Shakespearized" Pokemon description.
It gets the name of a Pokemon as input and returns the description
I Took the liberty to use part of a [wrapper nuget package](https://gitlab.com/PoroCYon/PokeApi.NET) to access the [pokeapi](https://pokeapi.co/), as it is published under the "DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE", but I had to make some changes because it is not updated with the current datastructure returnet by the service.

## Installation

The project has been created using VS 2019 Community and deployed as Linux docker container, so Docker desktop should be installed in order to run it.
Once deployed it can be called with https://localhost:32782/pokemon/{pokemon name}

```bash
pip install foobar
```

## Usage

```python
import foobar

foobar.pluralize('word') # returns 'words'
foobar.pluralize('goose') # returns 'geese'
foobar.singularize('phenomena') # returns 'phenomenon'
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
