
# IOException/JSONException by exceeding the size limit
- run both checkout and orderProcessor apps in self hosted mode.

- use the following body to call the checkout API: 

```text
curl --location 'http://localhost:7001/checkout' \
--header 'Content-Type: application/json' \
--data '{
    "size": 80000,
    "delay": 0
}'
```
size parameter in body with 80000 and more will produce a response larger than 4 mb. (default is 4 mb)
- when the response size is larger than configured daprHTTPMaxRequestSize the dapr runtime of the callee  serivce (OrderProcessor dapr) returns back the truncated response with 200 response.
![OrderProcessor dapr - 200](/images/exceed-size-order-processor-dapr.png)
- The caller dapr runtime (checkout dapr) also writes a warning but sends 200 and truncated json message to the app which cause the jsonException while deserialization.
  
    Checkout dapr:
    ![Checkout dapr - error](/images/exceed-size-checkout-dapr-error.png)
    Checkout app:
    ![Checkout app - error](/images/load-test-checkout-app-error.png)
  