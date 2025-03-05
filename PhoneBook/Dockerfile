FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY ./PhoneBook/*.csproj ./PhoneBook/
RUN dotnet restore ./PhoneBook/PhoneBook.csproj

COPY PhoneBook/ ./PhoneBook/
WORKDIR /app/PhoneBook
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/PhoneBook/out .
EXPOSE 8080
CMD ["dotnet", "PhoneBook.dll"]
