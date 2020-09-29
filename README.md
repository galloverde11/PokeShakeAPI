# PokeShakeAPI

PokeShakeAPI is an API library to get "Shakespearized" Pokemon description; it gets the name of a Pokemon as input and returns the description.
I Took the liberty to use part of a [wrapper nuget package](https://gitlab.com/PoroCYon/PokeApi.NET) to access the [pokeapi](https://pokeapi.co/), as it is published under the "DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE", but I had to make some changes because it is not updated with the current datastructure returnet by the service.

## Installation

The project has been created using VS 2019 Community and deployed as Linux docker container, so Docker desktop should be installed in order to run it.

## Configuration

There are some configuration parameters in appsettings.json

```jsonc
...
"Pokemon": {
    "DefaultSiteURL": "https://pokeapi.co", //Base URL
    "SpeciesEndpoint": "/api/v2/pokemon-species/", //Endpoint
    "UserAgent": "PokeShakeAPI (https://gitlab.com/galloverde11/PokeShakeAPI or a fork of it)", //User agent used for the call
    "RandomDescription": false, // If enabled it randomly returns one of the many description of the given pokemon (only the english ones)
    "UseCache": true, // If enabled memory caching is used
    "CacheSuffix": "pokecache_", // When caching is enabled, this suffix is added to the keys
    "ResourceNotFoundMsg": "It was not possible to get the description" // Error message returned when resource is not found
},
"Shakespeare": {
    "DefaultSiteURL": "https://api.funtranslations.com", //Base URL
    "SpeciesEndpoint": "/translate/shakespeare.json?text=", //Endpoint
    "UserAgent": "PokeShakeAPI (https://gitlab.com/galloverde11/PokeShakeAPI or a fork of it)", //User agent used for the call
    "UseCache": true, // If enabled memory caching is used
    "CacheSuffix": "shakecache_", // When caching is enabled, this suffix is added to the keys
    "ResourceNotFoundMsg": "It was not possible to get a translation" // Error message returned when resource is not found
},
...
```

## Usage

Once deployed it can be called as follow 

```bash
GET https://localhost:32782/pokemon/{pokemon name}
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
