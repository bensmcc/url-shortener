FROM mcr.microsoft.com/dotnet/core/sdk
COPY . /app
WORKDIR /app
RUN dotnet build
EXPOSE 5000

CMD cd UrlShortener && dotnet run