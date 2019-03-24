provider "aws" {
  skip_credentials_validation = true
  skip_metadata_api_check     = true
  s3_force_path_style         = true
  insecure                    = true
  access_key                  = "mock_access_key"
  secret_key                  = "mock_secret_key"
  region                      = "us-east-1"
  endpoints {
    dynamodb   = "http://localhost:4569"
    lambda     = "http://localhost:4574"
    apigateway = "http://localhost:4567"
  }

}

//TODO: need docker for runtime
resource "aws_lambda_function" "graphql" {
  function_name = "graphql-dotnet-pure"
  handler       = "Graphql.Api::graphql.api.ProgramLambda::Run"
  role          = "r1"
  runtime       = "dotnetcore2.0" //2.1 not supported yet
  filename      = "../graphql.api/bin/Release/netcoreapp2.1/graphql.api.zip"
}

resource "aws_api_gateway_rest_api" "api" {
  name = "gql"
}

resource "aws_api_gateway_resource" "api_proxy" {
  parent_id   = "${aws_api_gateway_rest_api.api.root_resource_id}"
  path_part   = "{proxy+}"
  rest_api_id = "${aws_api_gateway_rest_api.api.id}"
}

resource "aws_api_gateway_method" "api_proxy" {
  authorization = "NONE"
  http_method   = "ANY"
  resource_id   = "${aws_api_gateway_resource.api_proxy.id}"
  rest_api_id   = "${aws_api_gateway_rest_api.api.id}"
}

resource "aws_api_gateway_integration" "api_lambda" {
  http_method             = "${aws_api_gateway_method.api_proxy.http_method}"
  resource_id             = "${aws_api_gateway_method.api_proxy.resource_id}"
  rest_api_id             = "${aws_api_gateway_rest_api.api.id}"
  type                    = "AWS_PROXY"
  integration_http_method = "POST"
  uri                     = "${aws_lambda_function.graphql.invoke_arn}"
}

resource "aws_api_gateway_method" "proxy_root" {
  authorization = "NONE"
  http_method   = "ANY"
  resource_id   = "${aws_api_gateway_rest_api.api.root_resource_id}"
  rest_api_id   = "${aws_api_gateway_rest_api.api.id}"
}

resource "aws_api_gateway_integration" "lambda_root" {
  http_method             = "${aws_api_gateway_method.proxy_root.http_method}"
  resource_id             = "${aws_api_gateway_method.proxy_root.resource_id}"
  rest_api_id             = "${aws_api_gateway_rest_api.api.id}"
  type                    = "AWS_PROXY"
  integration_http_method = "POST"
  uri                     = "${aws_lambda_function.graphql.invoke_arn}"
}

resource "aws_api_gateway_deployment" "dev" {
  rest_api_id = "${aws_api_gateway_rest_api.api.id}"
  stage_name  = "dev"
  depends_on  = [
    "aws_api_gateway_integration.api_lambda",
    "aws_api_gateway_integration.lambda_root",
  ]
}


// http://localhost:4567/restapis/87A-Z84A-Z87A-Z4/dev/_user_request_/any
output "gw_dep_url" {
  value = "http://localhost:4567/restapis/${aws_api_gateway_deployment.dev.id}/${aws_api_gateway_deployment.dev.stage_name}/_user_request_/any"
}

output "gw_url" {
  value = "${aws_api_gateway_rest_api.api.id}"
}
