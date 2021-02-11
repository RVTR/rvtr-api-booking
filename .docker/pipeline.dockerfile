FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
LABEL maintainer="https://github.com/fredbelotte"

WORKDIR /workspace

COPY . .

CMD [ "dotnet", "RVTR.Booking.Service.dll" ]
