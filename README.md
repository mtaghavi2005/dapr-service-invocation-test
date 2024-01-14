# Service Invocation Test
This project is intended to be used for testing dapr service invocation and specifically for demonstrating certain issues or bugs.
![Error in checkout dapr](/images/service-invocation-overview.png)

- Reproduce IOException/JsonException under load testing [IOException LoadTesting](./ioexceptino-reproduce-guide.md).

- Reproduce IOException/JsonException by exceeding size limit (4 mb is default request/response size limit) [IOException exceed size](./ioexceptino-exceed-size-reproduce-guide.md).
