FROM microsoft/dotnet:2.1.302-sdk

ENV ASPNETCORE_URLS=http://*:5000

COPY . /var/www/aspnetcoreapp

WORKDIR /var/www/aspnetcoreapp/Articles.Api

CMD ["/bin/bash", "-c", "dotnet restore && dotnet run"]