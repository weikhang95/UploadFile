# Readme

Run the docker compose file to have postgres and minio serving.

```bash
docker-compose up
```

## Postgresql
1. Connect to postgres local server with 
   - Postgres user: postgres
   - Postgres password: 123
   - Postgres database： codingTest

## Minio
1. Login to Minio local server with
   - Minio user: MINIO
   - Password: 12345678

## UploadFileServer
You might need to run
``` dotnet ef database update ```
to create the table schema.
