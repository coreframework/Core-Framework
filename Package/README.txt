Core Frameworks configuration instructions.

1. Create database <your_database_name>.
2. Set environment setting value in web.config file (development, test, production):
		<appSettings>
			<add key="environment" value="development" />
		</appSettings> 
3. Set the same environment setting value int Build.proj file:
		<Environment Condition=" '$(Environment)' == '' ">development</Environment>
		
4. Change database connection string in Web\Config\database.yml file for the corresponding environment setting:

	development:
	  platform: sqlserver
	  host: database_host
	  database: dev_database_name
	  username: database_username
	  password: database_password
  
5. Run setup.bat script in the root directory to apply database migrations.
6. Create a web application <your_application_name> in IIS (.NET v.4, Integrated Pipeline mode) mapped to Web folder.
7. Type localhost/<your_application_name> in your browser.
8. To sign in click "Sign in" link in the top-right corner of the screen.
9. Default account with admin role is admin/admin.
10. Administrative panel available at localhost/<your_application_name>/admin.
11. To install modules go to "Modules' section of the administrative panel and click "Install" link of the appropriate module.