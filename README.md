## Actor Models with MS Orleans and ASP.NET Core 8 (Blazor and Web API) 
![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/6db2bd87-79e9-4d22-9e75-e722e9841e51)
- MS Orleans: 
  - https://github.com/dotnet/orleans 
### Deployment Model: **Azure Cloud**

![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/b7b43d0d-29af-4c46-8866-1ea5e45adfa1)

![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/1f15ded8-b26b-466b-b628-6c8f5c0ddc05)

**1. Solution**
   - Actor Models with MS Orleans (7.0.0) and ASP.NET Core 8 Blazor (.NET 8)

<img width="614" alt="image" src="https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/3fdad38c-d82e-474b-83ec-66645b1cbbeb">





     
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
         - Scalability through concurrency
         - Loose coupling through asynchronous messaging
         - State isolation
         - Location transparency
         
       - **Actor** => Fundamental unit of computaion; it has a private state
          - Allowed operations:
            - create another Actor
            - send a message
            - designnate how to handle the next message
           
           
            <img width="413" alt="image" src="https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/c800bad0-bab9-44fd-9ceb-90eed2f3c582">
            

             <img width="429" alt="image" src="https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/7de7b27d-b8cb-48a6-874c-d3477dd40545">
             
         - **3.1.1 When to use**
              - where several functions need to be run at once and have their results combined before final processing (fan-out/fan-in pattern)
              - where several actors can be “workers”, waiting for messages from the “publisher” -pub/sub pattern
              - where systems that need to manage several similar entities, like a multiplayer game where every player is represented as an actor
         - **3.1.2 When not to use**
              - when events order really matters, not promised in actor pattern
              - if one of the actors fails, you’ll have to deal with the concern of rolling back events
              - synchronous problems
              - error handling can be tricky with actors (“let it crash” philosophy)

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
  - **4.1 Frontend**
    - **CarsManager.Orleans.Web**
      - Based on : https://github.com/dotnet/samples/tree/main/orleans/ShoppingCart
    - **Home**
        ![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/c64e4b7d-dd83-4d88-91c7-8b1f4e8db8ef)
      
    - **Reservations**
        ![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/86b3d254-05bf-4385-af05-f7115233106c)
        ![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/b938b747-da2c-49fb-a9eb-22b26035bba3)

    - **Cars**
        ![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/43683a13-7b2e-41a6-88ae-7e36deee5acd)
      
    - **Booked Car Cart**
        ![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/3b0bdc5f-5657-49e1-b02e-8d375b31e1fa)
        ![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/15cf547c-858b-4139-80dc-255cff25f3b7)
        ![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/c4f79f05-824e-4e11-901f-7bbc9ce4d319)


    - **Tracker**
        ![image](https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/15501bae-23c2-44b3-ae29-e7b587ab15d6)
        - Used from: https://learn.microsoft.com/en-us/samples/dotnet/samples/orleans-gps-device-tracker-sample/

 - **4.2 Backend**
     - **CarsManager.Orleans.Silo**
     - **CarsManager.Orleans.Infrastructure**
     - **CarsManager.Orleans.Grains**
     - **CarsManager.Orleans.Domain**

  - **4.3 Additional applications**
     - **CarsManager.Orleans.Devices.Signal.Gen**
         - Cars position simulation
  
     - **CarsManager.Orleans.Dashborad**
         - Orleans Grains Dashboard
      
<img width="1259" alt="image" src="https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/60525df7-6749-4a45-9bd1-44c1e9e5011c">



 
 **5. Azure Cloud Deployment**
<img width="902" alt="image" src="https://github.com/skowragn/Actor_Models_with_MSOrleans/assets/97020391/926b4bf2-3234-4fa6-b1fc-7ca3b765ee26">


 
         - To be done asap

