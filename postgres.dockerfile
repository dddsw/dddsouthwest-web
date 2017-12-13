FROM postgres:9.4

ADD init.sql /docker-entrypoint-initdb.d/