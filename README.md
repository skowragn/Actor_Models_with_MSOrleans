## Actor Models with MS Orleans and ASP.NET Core 8 (Blazor and Web API) 
![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/6db2bd87-79e9-4d22-9e75-e722e9841e51)
- MS Orleans: 
  - https://github.com/dotnet/orleans 
### Deployment Model: **Azure Cloud**

![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/b7b43d0d-29af-4c46-8866-1ea5e45adfa1)

![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/1f15ded8-b26b-466b-b628-6c8f5c0ddc05)

**1. Solution**
   - Actor Models with MS Orleans (7.0.0) and ASP.NET Core 8 Blazor (.NET 8)
     
**2. References**
 - The following PoC has been done by using Microsoft source code examples as reference:
     - MS Orleans eShop: 
       - https://learn.microsoft.com/en-us/dotnet/orleans/deployment/
       - https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service
    - GPSTracker with Orleans:
       - https://learn.microsoft.com/en-us/samples/dotnet/samples/orleans-gps-device-tracker-sample/
    - Shoppining Cart with Orleans:
       - https://github.com/dotnet/samples/tree/main/orleans/ShoppingCart
          - The following open3rd party-source projects are using regarding Blazor
            - MudBlazor: Blazor Component Library based on Material design
              - https://github.com/MudBlazor/MudBlazor): .
            - Blazorators: Source-generated packages for Blazor JavaScript interop
              - https://github.com/IEvangelist/blazorators)
   
    - eShop with new ASP.NET Core 8 Blazor:
       - https://github.com/dotnet/eshop
    - MS Orleans on Azure Cloud: 
       - https://github.com/bradygaster/OrleansOnAzureContainerApps
       - https://github.com/bradygaster/OrleansOnAzureAppService
    - MS Orleans Dashborad:
       - https://github.com/OrleansContrib/OrleansDashboard/

   **3. Actor Model**
     - **3.1 Actor Model pattern**
     
       - **Actor Model** => Conceptual model of concurrent computation (from 1973)
       - **Actor** => Fundamental unit of computaion; it has a private state
          - Allowed operations:
            - create another Actor
            - send a message
            - designnate how to handle the next message

     - **3.2 Links**
      - https://blog.softwaremill.com/actor-model-and-event-sourcing-aa00993d2f1e
      - https://finematics.com/actor-model-explained/
      - https://www.theserverside.com/blog/Coffee-Talk-Java-News-Stories-and-Opinions/How-the-Actor-Model-works-by-example
      - https://www.oreilly.com/library/view/applied-akka-patterns/9781491934876/ch01.html 
      - Drinking a river of IoT data with Akka NET - Hannes Lowette - NDC Oslo 2021
         - https://www.youtube.com/watch?v=05Q837IX6M0
      - https://akshantalpm.github.io/Actor-Model-For-IoT/ 
      - https://stately.ai/blog/what-is-the-actor-model  

 **4. Current solution**

