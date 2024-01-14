
# IOException / JSONException under load testing
1. Download JMeter:
   - Go to JMeter Downloads (https://jmeter.apache.org/download_jmeter.cgi)
   - Download `apache-jmeter-5.6.3_src.zip`

2. Install `tye` dotnet tool if not available:
   - Install the latest version
   https://github.com/dotnet/tye/blob/main/docs/getting_started.md#working-with-ci-builds)

3. Execute `tye run`:
   - Ensure the configuration file `tye.yaml` is present
   - Run `tye run`

4. Extract `apache-jmeter-5.6.3_src.zip` and open JMeter:
   - Navigate to the extracted folder
   - Open `bin\ApacheJMeter.jar`

5. Open the JMeter test plan configuration file:
   - File name: `load-test.jmx` from the root directory.

6. Start the load test:
   - The test will continue to call the checkout API until it fails.

   ![Run Load Testing until it fails.](/images/load-test-jmeter-500.png)

   - in tye dashboard (http://localhost:8000/) check the log of checkout-dapr
   ![Error in checkout dapr](/images/load-test-checkout-dapr-error.png)

   - in tye dashboard (http://localhost:8000/) check the log of checkout app
   ![Checkout action code](/images/load-test-checkout-action.png)
   By checking the source of action we can find out that the response status was 200 and response payload is also truncated.

### Use Dapr multi-app run   
If you prefer to use dapr multi-app run there is also dapr.yaml available in the root directory.

### Use another load testing tool   
By using another load test tool, use the following API:

```bash
curl --location 'http://localhost:7001/checkout' \
--header 'Content-Type: application/json' \
--data '{
    "size": 70000,
    "delay": 0
}'


