# RegisterApp
Customer Registration API


Customer Registration API written in .Net Core 3.1 and Unit Tests with NUnit Tests

1. DAL Project - Code First Migrations
2. BLL Project - Generic Repositories and Services
3. Models Project - Entities, ViewModels and Model Validators
4. Web API - Customer Registration API. Returns CustomerId passed by the consumer if sucessful.
            0 if saving to DB fails, -1 if validation fails and -2 for internal server error.
            
TODO:

1. Handle Json Serlization Errors.
2. Auto Mapper for API Models to DB Models. (DTOs)
