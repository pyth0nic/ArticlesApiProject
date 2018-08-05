FROM postgres:latest
EXPOSE 5432
COPY postgres-init.sql /var/scripts/postgres-init.sql

WORKDIR /var/scripts/

CMD ["/bin/bash", "-c", "psql postgres-init.sql"]