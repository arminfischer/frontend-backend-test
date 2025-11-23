# Search App

A full-stack document search application built with .NET 9.0 backend and React + Material UI frontend.

## Project Structure

```txt
search-app/
├── backend/               # .NET 9.0 Web API
│   ├── Controllers/       # API Controllers
│   ├── Models/           # Data models
│   ├── Services/         # Business logic
│   └── Program.cs        # Application entry point
├── frontend/             # React SPA
│   ├── src/
│   │   ├── pages/       # React pages/components
│   │   ├── App.jsx      # Main app component
│   │   └── main.jsx     # Entry point
│   ├── index.html       # HTML template
│   └── vite.config.js   # Vite configuration
└── .vscode/             # VS Code configuration
    ├── launch.json      # F5 debugging configuration
    └── tasks.json       # Build tasks
```

## Features

- **Search API**: `/api/search?query=<query>` - Search documents by keywords
- **Document Details API**: `/api/documents/<id>` - Get full document details
- **React SPA**: Modern UI with Material UI components
- **Real-time Search**: Search results update as you type
- **Responsive Design**: Works on desktop and mobile devices

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) (v18 or higher)
- [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

### Option 1: Run with F5 (Recommended for Development)

1. Open the project in Visual Studio Code
2. Press **F5** to start debugging
3. Choose "**Full Stack (Backend + Frontend Dev Server)**" from the dropdown
4. The app will automatically open at `http://localhost:3000`

### Option 2: Run Backend and Frontend Separately

**Backend:**

```powershell
cd backend
dotnet run
```

Backend runs on `http://localhost:5000`

**Frontend (in a new terminal):**

```powershell
cd frontend
npm install
npm run dev
```

Frontend runs on `http://localhost:3000`

## API Endpoints

### Search Documents

```txt
GET /api/search?query=<search-term>
```

Returns a list of matching documents with relevance scores.

**Example:**

```bash
curl http://localhost:5000/api/search?query=react
```

### Get Document Details

```txt
GET /api/documents/{id}
```

Returns full details of a specific document.

**Example:**

```bash
curl http://localhost:5000/api/documents/1
```

## Building for Production

### Build Frontend

```powershell
cd frontend
npm run build
```

This creates optimized static files in `frontend/dist/`

### Build Backend

```powershell
cd backend
dotnet publish -c Release
```

## Deployment to Azure Web App

### Deployment Prerequisites

- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- Azure subscription

### Steps

1. **Build the frontend:**

   ```powershell
   cd frontend
   npm install
   npm run build
   ```

2. **Copy frontend build to backend wwwroot:**

   ```powershell
   New-Item -ItemType Directory -Force -Path backend/wwwroot
   Copy-Item -Path frontend/dist/* -Destination backend/wwwroot/ -Recurse -Force
   ```

3. **Publish backend:**

   ```powershell
   cd backend
   dotnet publish -c Release -o ./publish
   ```

4. **Deploy to Azure:**

   ```powershell
   # Login to Azure
   az login
   
   # Create a resource group (if needed)
   az group create --name myResourceGroup --location eastus
   
   # Create an App Service plan
   az appservice plan create --name myAppServicePlan --resource-group myResourceGroup --sku B1 --is-linux
   
   # Create a Web App
   az webapp create --name mySearchApp --resource-group myResourceGroup --plan myAppServicePlan --runtime "DOTNET|9.0"
   
   # Deploy the app
   cd publish
   Compress-Archive -Path * -DestinationPath ../deploy.zip -Force
   cd ..
   az webapp deployment source config-zip --resource-group myResourceGroup --name mySearchApp --src deploy.zip
   ```

5. **Access your app:**

   ```txt
   https://mySearchApp.azurewebsites.net
   ```

### Alternative: Deploy via VS Code

1. Install the [Azure App Service extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azureappservice)
2. Build frontend and copy to backend wwwroot (steps 1-2 above)
3. Right-click on the `backend` folder in VS Code
4. Select "Deploy to Web App..."
5. Follow the prompts to create/select an Azure Web App

## Development Tips

- **Hot Reload**: Frontend changes auto-reload with Vite
- **Backend Watch**: Use `dotnet watch run` in backend folder for auto-restart on code changes
- **API Testing**: Use the REST Client extension in VS Code or Postman
- **Debugging**: Set breakpoints in VS Code and press F5 to debug backend code

## Technologies Used

- **Backend**: .NET 9.0, ASP.NET Core Web API
- **Frontend**: React 18, Material UI 5, React Router 6, Vite
- **Development**: Visual Studio Code, C#, JavaScript/JSX

## License

MIT
