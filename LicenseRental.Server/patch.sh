curl --location --request PATCH 'https://localhost:7233/background' \
--header 'Content-Type: application/json' \
--data-raw '{
    "IsEnabled": true
}'