FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
LABEL maintainer="https://github.com/fredbelotte"

WORKDIR /workspace

COPY . .

CMD [ "dotnet", "RVTR.Booking.Service.dll" ]
