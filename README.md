# Karma

Karma is a server-side project built with .NET 8, featuring a Clean architecture design pattern and unit testing. It also includes in-memory caching for efficient data retrieval. This project facilitates the interaction between job seekers and employers, allowing job seekers to upload their resumes and employers to browse through them to find suitable candidates for their job offers.

## Installation

1. Clone the repository to your local machine.

    ```bash
    git clone https://github.com/mhaz2000/karma.git
    ```

2. Navigate to the project directory.

    ```bash
    cd karma
    ```

3. Build the solution.

    ```bash
    dotnet build
    ```

4. Run the project.

    ```bash
    dotnet run
    ```

## Configuration

- **Database Connection**: Modify the database connection string in `appsettings.json` to connect to your desired database.
- **Log Connection**: Modify the log database connection string in `appsettings.json` to connect to your desired database.
- **Cache Configuration**: Adjust cache settings in `appsettings.json` according to your caching requirements.

## Testing

Karma includes unit tests to ensure the reliability and correctness of its functionality.

```bash
dotnet test
```

## Contributing

Contributions are welcome! If you'd like to contribute to Karma, please fork the repository, make your changes, and submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).



