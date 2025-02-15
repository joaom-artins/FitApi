### Arquivo: sqlserver-entrypoint.sh
```bash
#!/bin/bash

# Verifica se a variável SA_PASSWORD está definida
if [[ -z "$SA_PASSWORD" ]]; then
    echo "Erro: A variável de ambiente SA_PASSWORD não está definida."
    exit 1
fi

/opt/mssql/bin/sqlservr &

ERRCODE=1
i=0

while [[ $i -lt 90 ]] && [[ $ERRCODE -ne 0 ]]; do
    echo "Tentando conectar ao SQL Server. Tentativa $i..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1
    ERRCODE=$?
    if [[ $ERRCODE -eq 0 ]]; then
        echo "Conectado ao SQL Server."
        break
    fi
    ((i++))
    sleep 2  # Espera um pouco mais para garantir inicialização
done

if [[ $ERRCODE -ne 0 ]]; then 
    echo "O SQL Server demorou mais de 90 segundos para iniciar ou falhou ao conectar."
    exit 1
fi

# Executa o script SQL
echo "Executando script SQL..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d master -i /usr/src/app/sqlserver.sql
if [[ $? -ne 0 ]]; then
    echo "Falha ao executar o script SQL."
    exit 1
fi

echo "Script SQL executado com sucesso."
wait
```