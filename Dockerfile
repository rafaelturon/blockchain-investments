FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app/src/Blockchain.Investments.Web

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

CMD dotnet run --server.urls http://0.0.0.0:$PORT
