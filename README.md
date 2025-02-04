# Azure Resume Project

This project is a static website displaying my resume, with the backend powered by Azure Function App and CosmosDB. The frontend is built using PURE JavaScript, and the data is stored in a non-relational database (CosmosDB) to track the number of times the website has been accessed.

## Project Overview

- **Frontend:** PURE JavaScript
- **Backend:** Azure Function App (C#)
- **Database:** CosmosDB (Non-relational)
- **Hosting:** Azure Storage Blob for static files

## How It Works

1. **Frontend Interaction:**  
   Every time a user accesses the website, a request is sent to the Azure Function App (the backend).
   
2. **Backend Interaction:**  
   The Azure Function App queries CosmosDB for the current count of site visits.

3. **Response:**  
   The Function App returns the visit count to the frontend, where it is displayed to the user. The count is then incremented in the CosmosDB document to update the number of accesses.

4. **Database Update:**  
   The visit count in CosmosDB is updated each time the site is accessed, ensuring that the counter reflects the most recent number of visitors.

## Access the Project

You can access the live project here:  
[Azure Resume Project](https://azureresumetzur1.z39.web.core.windows.net/)

## Project Diagram

A visual representation of the architecture and flow of the project can be found here:  
![Project Diagram](https://github.com/user-attachments/assets/89d1d441-4d2d-4b1d-96ee-cf18a3856bf1)


## Technologies Used

- **Azure Function App** (C#)  
  Backend functionality to handle API requests and interact with CosmosDB.
  
- **CosmosDB**  
  A non-relational database to store the site access count.

- **JavaScript**  
  Used for building the frontend of the static website.

- **Azure Storage Blob**  
  Used to host the static files (HTML, CSS, JS).
