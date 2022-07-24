# BraveFish.WorkflowMaster

A State Management Web API for workflow operations using `.NET6` with `RabbitMq` and `PostgreSQL`.

## Plan

A plan is an entity saved to the database to record a plan definition. A cURL request is provided below to register a sample Plan Definition. 

```bash
curl --request POST \
  --url http://localhost:5000/api/workflow/plan/register \
  --header 'Accept: application/vnd.github.v3+json' \
  --header 'Content-Type: application/json' \
  --data '{
	"name": "PartnerOnbdgFlow",
	"description": "Workflow for PartnerOnbdg",
	"planDefinition": {
		"items": [
			{
				"fromState": "INIT",
				"stateName": "VALIDATING",
				"itemActions": [
					{
						"queueName": "rabbitLogger",
						"addressName": "firstAddress"
					}
				]
			},
			{
				"fromState": "VALIDATING",
				"stateName": "VERIFYING",
				"itemActions": [
					{
						"queueName": "rabbitLogger",
						"addressName": "firstAddress"
					},
					{
						"queueName": "rabbitLogger",
						"addressName": "secondAddress"
					}
				]
			}
		]
	}
}'
```

The response will look something like this:
```json
{
	"data": {
		"id": "PLAN-e43bd22d-9589-4d40-97e4-0ab6d8d9a2a4",
		"name": "PartnerOnbdgFlow",
		"description": "Workflow for PartnerOnbdg",
		"planDefinition": {
			"items": [
				{
					"fromState": "INIT",
					"stateName": "VALIDATING",
					"itemActions": [
						{
							"queueName": "rabbitLogger",
							"addressName": "firstAddress"
						}
					]
				},
				{
					"fromState": "VALIDATING",
					"stateName": "VERIFYING",
					"itemActions": [
						{
							"queueName": "rabbitLogger",
							"addressName": "firstAddress"
						},
						{
							"queueName": "rabbitLogger",
							"addressName": "secondAddress"
						}
					]
				}
			]
		},
		"createdAt": "2022-07-23T23:45:39.3590562Z",
		"isDeprecated": false
	},
	"status": null
}
```

## Pipeline

A pipeline is a run job record of an existing plan. Users can first register their plan and then reference that plan for execution with parameters. The parameters will be saved to the pipeline. By default, the initial status will always be `INIT`. So your first `Item` in the planDefiniton should reference `INIT` as its `fromState` parameter and your `stateName` is your starting state. 

The `itemActions` field is a list of `ItemAction` objects. The `ItemAction` object fields hold the `queueName` & `addressName` field, this is for the workflow webapi to know which rabbitMQ address it's supposed to send the input parameters to.

Below is shown an example workflow initialization request:

```bash
curl --request POST \
  --url http://localhost:5000/api/workflow/pipeline/init \
  --header 'Accept: application/vnd.github.v3+json' \
  --header 'Content-Type: application/json' \
  --data '{
	"planId": "PLAN-e43bd22d-9589-4d40-97e4-0ab6d8d9a2a4",
	"params": {
		"username": "kresnofatih",
		"email": "kresnofatih@google.co.us"
	}
}'
```

The response will look something like this:
```json
{
	"data": {
		"id": "PPLN-4d56c88e-c78c-4b60-96b6-b62d4fa18f80",
		"planId": "PLAN-e43bd22d-9589-4d40-97e4-0ab6d8d9a2a4",
		"planDefinition": {
			"items": [
				{
					"fromState": "INIT",
					"stateName": "VALIDATING",
					"itemActions": [
						{
							"queueName": "rabbitLogger",
							"addressName": "firstAddress"
						}
					]
				},
				{
					"fromState": "VALIDATING",
					"stateName": "VERIFYING",
					"itemActions": [
						{
							"queueName": "rabbitLogger",
							"addressName": "firstAddress"
						},
						{
							"queueName": "rabbitLogger",
							"addressName": "secondAddress"
						}
					]
				}
			]
		},
		"currentStatus": "INIT",
		"params": {
			"username": "kresnofatih",
			"email": "kresnofatih@google.co.us"
		},
		"createdAt": "2022-07-23T23:46:17.3007601Z"
	},
	"status": null
}
```

## Transaction

To move from one state to another, you will need to call the pipeline shift request. The request should include a `pipelineId` and a `toState` as the desired next state. The params field is a `Dictionary<string, string>` for users to add additional params to the pipeline. Remember, it only accepts params with new keys, you can always reuse old params as they are always included in the message queue delivered by the workflow service or add new keys if there's an updated version of that param. A sample CURL request is given below:

```bash
curl --request POST \
  --url http://localhost:5000/api/workflow/pipeline/shift \
  --header 'Accept: application/vnd.github.v3+json' \
  --header 'Content-Type: application/json' \
  --data '{
	"pipelineId": "PPLN-4d56c88e-c78c-4b60-96b6-b62d4fa18f80",
	"toState": "VERIFYING",
	"params": {
		"randomString": "j02f2nfjhf2bfo394j430",
        "newerEmail": "kresnofatih@aws.co.us"
	}
}'
```

The response will show an updated version of that pipeline record from the database like so:

```json
{
	"data": {
		"id": "PPLN-4d56c88e-c78c-4b60-96b6-b62d4fa18f80",
		"planId": "PLAN-e43bd22d-9589-4d40-97e4-0ab6d8d9a2a4",
		"planDefinition": {
			"items": [
				{
					"fromState": "INIT",
					"stateName": "VALIDATING",
					"itemActions": [
						{
							"queueName": "rabbitLogger",
							"addressName": "firstAddress"
						}
					]
				},
				{
					"fromState": "VALIDATING",
					"stateName": "VERIFYING",
					"itemActions": [
						{
							"queueName": "rabbitLogger",
							"addressName": "firstAddress"
						},
						{
							"queueName": "rabbitLogger",
							"addressName": "secondAddress"
						}
					]
				}
			]
		},
		"currentStatus": "VERIFYING",
		"params": {
			"username": "kresnofatih",
			"email": "kresnofatih@google.co.us",
            "randomString": "j02f2nfjhf2bfo394j430",
            "newerEmail": "kresnofatih@aws.co.us"
		},
		"createdAt": "2022-07-23T23:46:17.30076Z"
	},
	"status": null
}
```

Notice the `params` field has more key value pairs now.

Everytime a shift request is successful, the workflow webapi will save a Transaction record to log state changes of the pipeline.