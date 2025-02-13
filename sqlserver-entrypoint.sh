#!/bin/bash
/opt/mssql/bin/sqlservr &

ERRCODE=1
i=0

while [[ $i -lt 60 ]] && [[ $ERRCODE -ne 0 ]]; do
    echo "Attempting to connect to SQL Server. Attempt $i..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1
    ERRCODE=$?
    if [[ $ERRCODE -eq 0 ]]; then
        echo "Successfully connected to SQL Server."
        break
    fi
    ((i++))
    sleep 1
done

if [[ $ERRCODE -ne 0 ]]; then 
    echo "The SQL Server took more than 60 seconds to initialize or failed to connect in 60 attempts."
    exit 1
fi

echo "Executing SQL script..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d master -i /usr/src/app/sqlserver.sql
if [[ $? -ne 0 ]]; then
    echo "Failed to execute the SQL script."
    exit 1
fi

echo "SQL script executed successfully."
wait