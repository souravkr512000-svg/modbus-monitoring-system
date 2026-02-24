# 🚀 Complete Deployment Guide - EmployeeDashboard

Step-by-step instructions to deploy your EmployeeDashboard application to production hosting platforms.

---

## 📋 Table of Contents

1. [Render Deployment (Easiest - Recommended)](#render-deployment)
2. [Azure App Service (Best for .NET)](#azure-app-service)
3. [GitHub Pages (Static Only)](#github-pages)
4. [Docker Deployment](#docker-deployment)

---

## 🌐 Option 1: Render Deployment (Easiest)

### Step 1: Create Render Account
1. Go to **https://render.com**
2. Click **"Get Started for Free"**
3. Sign up with your **GitHub account** (recommended)
4. Authorize Render to access your repositories

### Step 2: Create New Web Service
1. In Render dashboard, click **"New +"** button
2. Select **"Web Service"**
3. Click **"Connect GitHub"** if not already connected
4. Select your repository: **`modbus-monitoring-system`**

### Step 3: Configure Service Settings

**Basic Settings:**
- **Name**: `employee-dashboard` (or your preferred name)
- **Region**: Choose closest to you (e.g., `Oregon (US West)`)
- **Branch**: `master` (or `main`)

**Build & Deploy:**
- **Root Directory**: Leave empty (or set to `EmployeeDashboard` if deploying only that)
- **Environment**: `Docker` or `Native`
- **Build Command**: 
  ```bash
  cd EmployeeDashboard && dotnet restore && dotnet publish -c Release -o ./publish
  ```
- **Start Command**: 
  ```bash
  cd EmployeeDashboard && dotnet EmployeeDashboard.dll --urls http://0.0.0.0:$PORT
  ```

**OR Use Docker (Recommended):**
- **Dockerfile Path**: `EmployeeDashboard/Dockerfile`
- **Docker Build Context**: `EmployeeDashboard`

### Step 4: Set Environment Variables
Click **"Advanced"** → **"Environment Variables"** → Add:

| Key | Value |
|-----|-------|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `ASPNETCORE_URLS` | `http://0.0.0.0:$PORT` |
| `PORT` | `10000` (or leave Render's default) |

### Step 5: Deploy
1. Click **"Create Web Service"**
2. Wait 5-10 minutes for first deployment
3. Watch the build logs in real-time
4. Once deployed, you'll get a URL like: `https://employee-dashboard.onrender.com`

### Step 6: Verify Deployment
1. Open your Render URL in browser
2. You should see the login page
3. Test with demo credentials:
   - Email: `admin@company.com`
   - Password: `Admin@123`

### ✅ Render Deployment Complete!

**Your app is live at:** `https://your-app-name.onrender.com`

---

## ☁️ Option 2: Azure App Service (Best for .NET)

### Step 1: Create Azure Account
1. Go to **https://azure.microsoft.com/free/**
2. Click **"Start free"**
3. Sign up with Microsoft account
4. Verify your identity (credit card required but won't be charged for free tier)

### Step 2: Create App Service
1. Go to **Azure Portal**: https://portal.azure.com
2. Click **"Create a resource"**
3. Search for **"Web App"**
4. Click **"Create"**

### Step 3: Configure App Service

**Basics Tab:**
- **Subscription**: Select your subscription
- **Resource Group**: Create new → Name: `employee-dashboard-rg`
- **Name**: `employee-dashboard-{your-unique-id}` (must be globally unique)
- **Publish**: `Code`
- **Runtime stack**: `.NET 8`
- **Operating System**: `Linux` (recommended) or `Windows`
- **Region**: Choose closest region
- **App Service Plan**: 
  - Click **"Create new"**
  - Name: `employee-dashboard-plan`
  - Pricing tier: **Free F1** (for testing) or **Basic B1** (for production)

Click **"Review + create"** → **"Create"**

### Step 4: Get Deployment Credentials
1. Go to your App Service in Azure Portal
2. Click **"Deployment Center"** in left menu
3. Click **"Settings"** tab
4. Under **"Source"**, select **"GitHub"**
5. Authorize Azure to access GitHub
6. Select:
   - **Organization**: Your GitHub username
   - **Repository**: `modbus-monitoring-system`
   - **Branch**: `master`
   - **Build provider**: `GitHub Actions`
7. Click **"Save"**

### Step 5: Configure Application Settings
1. Go to **"Configuration"** in left menu
2. Click **"Application settings"** tab
3. Click **"+ New application setting"**
4. Add these settings:

| Name | Value |
|------|-------|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `WEBSITE_RUN_FROM_PACKAGE` | `1` |

5. Click **"Save"**

### Step 6: Update GitHub Actions Workflow
1. Go to your GitHub repository
2. Navigate to **".github/workflows/deploy.yml"**
3. Uncomment the Azure deployment section:

```yaml
- name: Deploy to Azure
  uses: azure/webapps-deploy@v3
  with:
    app-name: 'employee-dashboard-{your-app-name}'
    publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
    package: ./publish
```

4. Get Publish Profile:
   - Go to Azure Portal → Your App Service
   - Click **"Get publish profile"**
   - Copy the entire XML content

5. Add GitHub Secret:
   - Go to GitHub repo → **Settings** → **Secrets and variables** → **Actions**
   - Click **"New repository secret"**
   - Name: `AZURE_PUBLISH_PROFILE`
   - Value: Paste the XML content
   - Click **"Add secret"**

### Step 7: Update deploy.yml
Edit `.github/workflows/deploy.yml`:

```yaml
- name: Deploy to Azure
  uses: azure/webapps-deploy@v3
  with:
    app-name: 'employee-dashboard-{your-actual-app-name}'
    publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
    package: ./publish
```

Replace `{your-actual-app-name}` with your Azure app name.

### Step 8: Trigger Deployment
1. Commit and push the updated `deploy.yml`:
   ```bash
   git add .github/workflows/deploy.yml
   git commit -m "Configure Azure deployment"
   git push origin master
   ```

2. Go to **Actions** tab in GitHub
3. Watch the deployment workflow run
4. Once complete, your app will be live!

### Step 9: Access Your App
Your app URL will be: `https://employee-dashboard-{your-id}.azurewebsites.net`

### ✅ Azure Deployment Complete!

---

## 🐳 Option 3: Docker Deployment (Any Platform)

### Step 1: Build Docker Image Locally (Test)
```bash
cd EmployeeDashboard
docker build -t employee-dashboard:latest .
docker run -p 8080:8080 employee-dashboard:latest
```

### Step 2: Push to Docker Hub
1. Create account at **https://hub.docker.com**
2. Login:
   ```bash
   docker login
   ```
3. Tag your image:
   ```bash
   docker tag employee-dashboard:latest your-username/employee-dashboard:latest
   ```
4. Push:
   ```bash
   docker push your-username/employee-dashboard:latest
   ```

### Step 3: Deploy to Any Platform

**Render:**
- Select **"Docker"** as environment
- Dockerfile path: `EmployeeDashboard/Dockerfile`

**Azure Container Instances:**
- Create new resource → **Container Instances**
- Use your Docker Hub image URL

**AWS ECS/Fargate:**
- Push to AWS ECR
- Create ECS service with your image

---

## 📝 Post-Deployment Checklist

After deploying, verify:

- [ ] Application loads without errors
- [ ] Login page displays correctly
- [ ] Can login with demo credentials
- [ ] Dashboard displays employee data
- [ ] CRUD operations work
- [ ] HTTPS is enabled (if available)
- [ ] Environment variables are set correctly
- [ ] Database is accessible (if using external DB)

---

## 🔧 Troubleshooting

### Issue: Build Fails
**Solution:**
- Check build logs in hosting platform
- Verify .NET SDK version matches (8.0)
- Ensure all dependencies are in `.csproj`

### Issue: App Shows 500 Error
**Solution:**
- Check application logs
- Verify `ASPNETCORE_ENVIRONMENT=Production`
- Check database connection (if using external DB)
- Review startup errors in logs

### Issue: Static Files Not Loading
**Solution:**
- Ensure `wwwroot` folder is included in publish
- Check `UseStaticFiles()` is called in `Program.cs`
- Verify file paths are correct

### Issue: Can't Access Database
**Solution:**
- For In-Memory DB: Data resets on restart (expected)
- For SQL Server: Check connection string
- Verify firewall rules allow database access

---

## 🎯 Recommended Platform

**For Beginners:** **Render** (easiest setup, free tier)
**For Production:** **Azure App Service** (best .NET support, scalable)
**For Docker:** **Any platform** (most flexible)

---

## 📞 Need Help?

1. Check platform-specific documentation
2. Review application logs
3. Test locally first before deploying
4. Use staging environment for testing

---

**Your EmployeeDashboard is now live! 🎉**
