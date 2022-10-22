[![Database](https://github.com/mif-az/Ultra-Saver/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mif-az/Ultra-Saver/actions/workflows/dotnet.yml)

<hr/>

## Setup

### Requirements

- NodeJS
- dotnet

### How to launch

1. Clone repo
1. Go to the ClientApp folder
1. Run
   ```bash
    npm install
   ```
1. Come back to the main folder
1. Run
   ```bash
   dotnet run
   ```

### How to deal with secrets

To add a secret run where `<SECRET>` is the name of the secret and `<VALUE>` is the actual value

```bash
dotnet user-secrets set "<SECRET>" "<VALUE>"
```

If you want to check the secrets you've already set you can run

```bash
dotnet user-secrets list
```

### CRUD and Auth API

There is a high chance you will need info from a backend API route that requires authorization - for that you will need to use Auth API.

1. Import UserContext and Auth API from UserProvider: `import { UserContext, authApi } from '../contexts/UserProvider';`
1. Get the global user state: `const [user, setUser] = useContext(UserContext);`
1. Call any CRUD method you need:
   1. GET method: `authApi(user).get(<url>)`
   1. POST method (default behavior is upsert\*) `authApi(user).post(<url>, <body>)` It is important that `<body>` matches the database model
   1. DELETE method `authApi(user).post(<url>, <body>, "DELETE")` only the unique identifier fields are enough to provide in `<body>`. These fields can vary but the usual one is `"id"`

> \*upsert: check if an entry with the unique identifier exists in the database -> if so, update with provided data; otherwise create a new entry in the database using provided data
