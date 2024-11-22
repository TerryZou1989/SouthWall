#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#ARM64
FROM mcr.microsoft.com/dotnet/aspnet:8.0.0
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
WORKDIR /app
EXPOSE 80
COPY . .
ENV ASPNETCORE_URLS http://+:80
ENTRYPOINT ["dotnet","SouthWall.dll"]