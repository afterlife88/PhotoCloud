# PhotoCloud

Internal Avanade DK workshop around service bus, async microservices communication, pubsub and queue functionality  

## Requirements 
You should develop a solution to upload a photo and share it with a list of friends.
The client side is developed on mobile (iOS), you should develop the server side upload process.

- For each photo uploaded, we can add other information: Date, Title, Author
- GPS location will be automatically detected from EXIF information stored in each photo
- All this information will be stored into a database
- The picture will be stored
    - It takes 1 second to upload a new photo and store it
- Most of the time (950 milliseconds) is needed to extract the GPS location from the photo
- The solution will be used to upload 50 new photos / second
- There are 200 threads allocated to the HTTP server

## Architecture Design with HTTP communication between microservices 
![img.png](img.png)
## Architecture Design with async communication between microservices using Service Bus
![img_1.png](img_1.png)