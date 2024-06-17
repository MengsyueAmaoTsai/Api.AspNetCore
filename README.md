# RichillCapital.Api

[![ASP.NET Web API Continuous Integration](https://github.com/MengsyueAmaoTsai/Api.AspNetCore/actions/workflows/ci.yml/badge.svg)](https://github.com/MengsyueAmaoTsai/Api.AspNetCore/actions/workflows/ci.yml)

[![ASP.NET Web API Continuous Deployment](https://github.com/MengsyueAmaoTsai/Api.AspNetCore/actions/workflows/cd.yml/badge.svg)](https://github.com/MengsyueAmaoTsai/Api.AspNetCore/actions/workflows/cd.yml)

###### Chart

```json
{
  "symbol": "{{ticker}}",
  "exchange": "{{exchange}}",
  "currentTime": "{{timenow}}",
  "open": "{{open}}",
  "high": "{{high}}",
  "low": "{{low}}",
  "close": "{{close}}",
  "volume": "{{volume}}",
  "time": "{{time}}",
  "interval": "{{interval}}"
}
```

###### Symbol

```json
{
  "currency": "{{syminfo.currency}}",
  "baseCurrency": "{{syminfo.basecurrency}}"
}
```

##### Order Info

```json
{
  "orderId": "{{strategy.order.id}}",
  "alertMessage": "{{strategy.order.alert_message}}",
  "comment": "{{strategy.order.comment}}",
  "price": "{{strategy.order.price}}",
  "contracts": "{{strategy.order.contracts}}",
  "tradeType": "{{strategy.order.action}}"
}
```

##### Strategy Information

```json
{
  "positionSize": "{{strategy.position_size}}",
  "previousMarketPositionSize": "{{strategy.prev_market_position_size}}",
  "previousMarketPosition": "{{strategy.prev_market_position}}",
  "marketPositionSize": "{{strategy.market_position_size}}",
  "marketPosition": "{{strategy.market_position}}"
}
```

```
{
  "sourceId": "TV-Long-Task",
  "currentTime": "{{timenow}}",
  "tradeType": "{{strategy.order.action}}",
  "exchange": "{{exchange}}",
  "symbol": "{{ticker}}",
  "price": "{{strategy.order.price}}",
  "marketPosition": "{{strategy.market_position}}",
  "marketPositionSize": "{{strategy.market_position_size}}",
  "previousMarketPosition": "{{strategy.prev_market_position}}",
  "previousMarketPositionSize": "{{strategy.prev_market_position_size}}"
}
```
