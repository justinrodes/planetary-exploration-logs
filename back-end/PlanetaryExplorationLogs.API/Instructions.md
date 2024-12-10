Note: For an easier viewing experience, download the Obsidian markdown editor and paste this file into a new note.

# 🚀 StarQuest Technologies: Planetary Exploration Logs API Challenge

Welcome, **StarQuest Explorer**! 🛸✨

Congratulations on reaching this pivotal stage of your journey to join the StarQuest Technologies team. We’re thrilled to have you onboard as a candidate for our Full Stack Web Developer position. Below you'll find all the information you need to embark on your mission: completing our Planetary Exploration Logs API and creating an Angular front-end to interface with it.

## 🪐 About StarQuest Technologies

At StarQuest Technologies, we push the boundaries of interstellar exploration. Our mission is to document and manage data from planetary expeditions, ensuring that every discovery is recorded and accessible for future generations. As part of our team, you'll contribute to building robust systems that support our explorers across the galaxy.

## 📂 Project Overview

You’ve been provided with an incomplete **.NET Core C# API** project designed to handle "Planetary Exploration Logs." Your task is twofold:

1. **Complete the API**: Implement the incomplete controller endpoints, which will be listed below in the **Task Breakdown** section.
2. **Create an Angular Front-End**: Develop an Angular application that interfaces seamlessly with the API, allowing users to interact with planetary exploration data.

### 🌌 Theme

Embrace the sci-fi spirit! Your work will help manage data from missions across diverse planets, tracking discoveries, mission details, and planetary information.

## 🛠️ Getting Started

### Prerequisites

- **.NET Core SDK** installed on your machine.
- **Visual Studio** or your preferred C# IDE.
- **Node.js** and **Angular CLI** installed for front-end development.

### Project Structure

- **API Project**: Contains Controllers, Models, DTOs, and Utilities.
- **Utility -> Snippets**: Includes Visual Studio snippet files for easy template creation. Use `query` and `command` shortcuts followed by the **TAB** key to generate CRQS Queries and Commands templates after installing these (optional). To install in Visual Studio:
	- Click *Tools -> Code Snippets Manager*
	- Click the *Import* button
	- Navigate to the project's *`\PlanetaryExplorationLogs.API\PlanetaryExplorationLogs.API\Utility\Snippets`* folder and select all three snippets
	- Click the *Finish* button
	- The snippets should now be available for use

## 🧩 Task Breakdown

### 1. Complete the API Endpoints

Navigate through the Controllers in the API project and implement the endpoints that are currently not functional. These endpoints return a `501 Not Implemented` status and need your expertise to bring them to life.

#### **DiscoveryController.cs**

- **GET** `api/discovery/{id}`: Retrieve a specific discovery by ID.
- **PUT** `api/discovery/{id}`: Update an existing discovery.
- **DELETE** `api/discovery/{id}`: Delete a discovery.

#### **MissionController.cs**

- **GET** `api/mission/{id}`: Retrieve a specific mission by ID.
- **POST** `api/mission`: Create a new mission.
- **PUT** `api/mission`: Update an existing mission.
- **DELETE** `api/mission/{id}`: Delete a mission by ID.
- **GET** `api/mission/{missionId}/discovery`: Retrieve all discoveries for a specific mission.
- **POST** `api/mission/{missionId}/discovery`: Create a new discovery under a specific mission.

*(Refer to the provided API project files for more details on each endpoint.)*

### 2. Develop the Angular Front-End

Create an Angular application that interacts with the completed API. Your front-end should allow users to:

- View, create, update, and delete missions and discoveries.
- Browse planetary information.
- Utilize dropdowns for selecting planets and discovery types.

## ⏰ Timeframe

You have **a limited time** to complete this challenge. Time management and efficient problem-solving will be key to successfully demonstrating your skills within this period. Your onboarding officer should have told you how much time you have to complete this.

## 📋 Submission Guidelines

Once you've completed the tasks:

1. **Push Your Code**: Ensure your API and Angular projects are properly committed to a Git repository.
2. **Walkthrough Presentation**: Be prepared to present and walk us through your implementation, highlighting your approach, challenges faced, and solutions devised.
3. **Documentation**: Include any additional documentation or notes that showcase your development process and decision-making.

## 📚 Resources & Tips

- **CRQS Patterns**: Utilize the existing CRQS Queries and Commands as examples to maintain consistency and best practices. See the `Requests/Commands` and the `Requests/Queries` folders.
- **Visual Studio Snippets**: Enhance your productivity by leveraging the snippets in the `Utility -> Snippets` folder. Simply type `query` or `command` and press **TAB** to generate templates.
- **API Documentation**: Familiarize yourself with the existing Models and DTOs to ensure accurate data handling and relationships.

## 🌟 Good Luck, Explorer!

Embark on this mission with enthusiasm and creativity. We’re excited to see how you contribute to the future of StarQuest Technologies. May the stars guide your code! ✨🛸