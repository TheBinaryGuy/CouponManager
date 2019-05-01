FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["CouponManager.Api/CouponManager.Api.csproj", "CouponManager.Api/"]
RUN dotnet restore "CouponManager.Api/CouponManager.Api.csproj"
COPY . .
WORKDIR "/src/CouponManager.Api"
RUN dotnet build "CouponManager.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CouponManager.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CouponManager.Api.dll"]
