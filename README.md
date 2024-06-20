# Desktop App for Form Submissions Management

# Description
This project is a Windows desktop application developed using Visual Studio and Visual Basic. It allows users to manage form submissions through a GUI with functionalities such as viewing existing submissions and creating new submissions.

# Details:
The app includes a main form with buttons for "View Submissions" and "Create New Submission". Clicking "Create New Submission" opens a form to input Name, Email, Phone Number, and a GitHub repo link. This form also controls a stopwatch for tracking time. The "View Submissions" button opens another form to navigate through previously submitted entries using "Next" and "Previous" buttons.

# Tools used:
Visual Studio: Integrated development environment used for designing and coding the Windows Desktop App.
Visual Basic: Programming language utilized for implementing the application logic and user interface functionalities.

[![Visual Studio & Visual Basic](https://skillicons.dev/icons?i=visualstudio,vb)](https://skillicons.dev)

# Screenshots :
![Screenshot](https://github.com/Rushi018/Desktop-App-for-Form-Submissions-Management/blob/main/SubmissionApp/Screenshot%202024-06-21%20005920.png)
![Screenshot](https://github.com/Rushi018/Desktop-App-for-Form-Submissions-Management/blob/main/SubmissionApp/Screenshot%202024-06-21%20010255.png)
![Screenshot](https://github.com/Rushi018/Desktop-App-for-Form-Submissions-Management/blob/main/SubmissionApp/Screenshot%202024-06-21%20010440.png)
![Screenshot](https://github.com/Rushi018/Desktop-App-for-Form-Submissions-Management/blob/main/SubmissionApp/Screenshot%202024-06-21%20010836.png)

# Backend Server for Desktop App

# Description
The backend server built with Express and TypeScript provides API endpoints for saving and retrieving submissions. It uses a JSON file (db.json) as a local database to store submissions.

# Endpoints
1. /ping
  A GET request that always returns true.

2. /submit
  A POST request used to save submissions. Requires parameters: "name", "email", "phone", "github_link", and "stopwatch_time".

3. /read
  A GET request with a query parameter "index" (0-indexed) to retrieve the (index+1)th form submission.

# JSON Database Structure (`db.json`)

```json
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phone": "123-456-7890",
  "github_link": "https://github.com/johndoe",
  "stopwatch_time": "01:30:00"
}
```

# Commands Used

```bash
# 1. Install TypeScript
npm install -g typescript

# 2. Create a New Node.js Project
mkdir backend-server
cd backend-server
npm init -y

# 3. Install Necessary Packages
# Install Express and types for Express and Node.js
npm install express @types/express

# Install TypeScript as a development dependency
npm install --save-dev typescript

# 4. Running the Server
# Compile TypeScript files
npx tsc

# Start the server (assuming server.js is generated in ./dist directory)
node ./dist/server.js 
```
