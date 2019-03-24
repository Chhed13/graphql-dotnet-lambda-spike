#!/usr/bin/env bash

# https://medium.com/@geekrodion/system-testing-localstack-terraforms-37b31ba99310

export SERVICES=s3,lambda
export DEFAULT_REGION=us-east-1
localstack start --docker

terraform init
terraform apply -lock=false -auto-approve

# https://github.com/localstack/localstack/issues/370