## Instructions

Hi!😊

This project was built using [.NET 6](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks) I have never used Golang before so didn't want to take days learning the basics and then implementing the solution for the test.

You will need VS Code and .NET SDK. You can download the .NET SDK [here](https://dotnet.microsoft.com/en-us/download). As of the time of this test, the LTS version is .NET 6, but if for some reason it doesn't work. You can find all versions [here](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks).

## Notes on Postgres
I wanted to make the project setup as easy as possible for you, so I am using a cloud based product for Postgres. That way there is no script or migration to run. Heroku offers a free tier. The credentials can be found in the appsettings.json file if needed to connect with a UI tool to see the schema, but please don't share them publicly. 😁

## Running the API
Open up a terminal and from the root folder of the project run `cd .\auth.api\` to change directory to the main source code. Then run `dotnet build` to compile the code and restore 3rd party packages. Finally run `dotnet watch` to run the API in watch mode. Your browser should open to the Swagger page, but if it does not, you can copy paste this link: https://localhost:7088/swagger/index.html

Swagger comes in handy to explore the structure of the API and the Models/Dtos.

## Authentication header
I am aware the API instructions mentioned the token will be part of the `x-authentication-token` however, .Net has a strong opinion on using Token Bearer auth instead. If I wanted to use a custom header, I could have implemented a middleware in the request pipeline. However, I'm sure that would have put me over 6 hours time on the solution (hopefully, this is not a deal breaker). If you want me to implement the header let me know. I recommend using the postman collection I included which has the setup for the Token Bearer.

## Sending Requests
I have included a Postman collection in the root/postman folder. This collection has API tests and is setup with a runner that could be integrated in a CICD pipeline. If you would like to use this collection you will need to import in your postman client.
1. Open your postman workspace and click the import button ![](/Illustrations/ImportCollection1.png)
2. Click the Upload Files button and navigate to the root/postman folder in this repo. ![](/Illustrations/ImportCollection2.png)
3. You should be able to see all the available request in your workspace ![](/Illustrations/ImportCollection3.png)

If you want to use the postman runner to run all of these requests and the included API tests you can find more info [here](https://learning.postman.com/docs/running-collections/intro-to-collection-runs/).

## Running the Unit Tests
Open up a terminal and from the root folder of the project run `cd .\auth.api.test\` to change directory to the main testing source code. Then run `dotnet build` to compile the code and restore 3rd party packages. Finally run `dotnet test`.


## Thank You!😁
