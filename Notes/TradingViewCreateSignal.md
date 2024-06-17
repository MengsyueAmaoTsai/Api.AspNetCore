# TradingView Webhook JSON Format

## Chart Info

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
  "interval": "{{interval}}",
  "currency": "{{syminfo.currency}}",
  "baseCurrency": "{{syminfo.basecurrency}}"
}
```

## Order Info

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

## Strategy Information

```json
{
  "positionSize": "{{strategy.position_size}}",
  "previousMarketPositionSize": "{{strategy.prev_market_position_size}}",
  "previousMarketPosition": "{{strategy.prev_market_position}}",
  "marketPositionSize": "{{strategy.market_position_size}}",
  "marketPosition": "{{strategy.market_position}}"
}
```

## Current Webhook Format

```
{
  "sourceId": "<uniqueId>",
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
