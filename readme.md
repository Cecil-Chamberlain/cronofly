#**What you need:**
- .NET 5
- docker
- awscli
- cake `dotnet tool install --global Cake.Tool`


#**Steps to get up and running**

**Configure local aws credentials for localstack**
```
$ aws configure --profile localstack

AWS Access Key ID [None]: localstack
AWS Secret Access Key [None]: localstack
Default region name [None]: eu-west-1
Default output format [None]: json
```

**Boot up local infrastructure**

`dotnet cake --target=init`

**Run unit tests**

`dotnet test`

**Run project locally**

`dotnet run --project src/Cronofly`

**Teardown local infrastructure**

`dotnet cake --target=down`



#**How to use**

Type a link in the text box:

![image](https://user-images.githubusercontent.com/23498437/156443362-010b8f68-1cff-455f-accd-d4ab48d42fa8.png)

Click the button 'Get Short Link':

See the new link that has been generated:

![image](https://user-images.githubusercontent.com/23498437/156443491-eb5eefb9-2276-4cad-8b33-ac5a238e7dff.png)

Click to be redirected:

![image](https://user-images.githubusercontent.com/23498437/156443530-19c4f9d2-9ceb-4b70-93ae-fe2f439c7362.png)

Profit
