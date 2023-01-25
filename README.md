# backend

## installation

### windows

- Install MariaDB, HeidiSQL is installed with it. if not, install HeidiSQL. Use a password you can remember when installing mariadb.
- In HeidiSQL, create a new server named localhost, and use the password you used when installing mariadb. Use port 3306 and your localhost IP
- In the server tab, open a query and run this code: `SET PASSWORD FOR 'root'@'localhost' = PASSWORD('root');`
- Now open this project in VS and run the `update-database` command in package manager console.
- Delete HeidiSQL and install MySqlWorkBench
- If you need to remote your IP to a local network, run `npm run ri-standard` or `npm run ri-iis`

### Mac fags

- Open a command line in mac
- Install MariaDB by typing `brew install mariadb`
- Start the mysql server by typing `brew services start mariadb`
- Install MySqlWorkBench
- Connect to a server with local ip, port 3306, user 'root' and an empty password
- In the server tab, open a query and run this code: `SET PASSWORD FOR 'root'@'localhost' = PASSWORD('root');`
- Install entity framework by running `dotnet tool install --global dotnet-ef`
- init the database by running `dotnet ef database update --startup-project Portal --project Data`

Ik hoop dat deze ding grote blij werkt, ik weet niet alles precies.

### Arch Supremists

- `yay -Sy dotnet-runtime-6.0 dotnet-sdk-6.0 dotnet-targeting-pack-6.0 aspnet-runtime-6.0`
- `docker run -d -p 3306:3306 --name mariadb -e MYSQL_ROOT_PASSWORD=root mariadb`
- `dotnet tool install --global dotnet-ef`
- `dotnet ef database update --startup-project Portal --project Data`

## Default Credentials

Email: `artimmerman@landstede.nl`
Password: `testen`

## Migrations

### Add a migration

- cd into the `Data` folder
- Run `dotnet ef --startup-project ../EasyIntern-Backend/ migrations add <NameOfYourMigration>`
