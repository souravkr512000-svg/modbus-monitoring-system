# ⚡ Quick Deploy Guide - 5 Minutes to Production

## 🚀 Fastest Way: Render (Recommended for Beginners)

### Step 1: Sign Up (1 minute)
1. Go to **https://render.com**
2. Click **"Get Started for Free"**
3. Sign up with **GitHub** (one-click)

### Step 2: Create Service (2 minutes)
1. Click **"New +"** → **"Web Service"**
2. Connect your repo: **`modbus-monitoring-system`**
3. Fill in:
   - **Name**: `employee-dashboard`
   - **Environment**: `Docker`
   - **Dockerfile Path**: `EmployeeDashboard/Dockerfile`
   - **Docker Build Context**: `EmployeeDashboard`

### Step 3: Add Environment Variable (30 seconds)
Click **"Advanced"** → **"Environment"** → Add:
- **Key**: `ASPNETCORE_ENVIRONMENT`
- **Value**: `Production`

### Step 4: Deploy (1 minute)
1. Click **"Create Web Service"**
2. Wait 5-10 minutes
3. Done! ✅

**Your URL:** `https://employee-dashboard.onrender.com`

---

## ☁️ Professional Way: Azure (Best for .NET)

### Step 1: Create Azure Account
- Go to **https://azure.microsoft.com/free/**
- Sign up (free tier available)

### Step 2: Create Web App
1. Azure Portal → **"Create a resource"**
2. Search **"Web App"** → **Create**
3. Fill:
   - **Name**: `employee-dashboard-{unique-id}`
   - **Runtime**: `.NET 8`
   - **OS**: `Linux`
   - **Plan**: `Free F1`

### Step 3: Connect GitHub
1. App Service → **"Deployment Center"**
2. Source: **GitHub**
3. Select repo: `modbus-monitoring-system`
4. Branch: `master`
5. Build: **GitHub Actions**

### Step 4: Add Secret
1. GitHub repo → **Settings** → **Secrets** → **Actions**
2. New secret: `AZURE_PUBLISH_PROFILE`
3. Value: Get from Azure Portal → App Service → **"Get publish profile"**

### Step 5: Update deploy.yml
Edit `.github/workflows/deploy.yml`:
```yaml
- name: Deploy to Azure
  uses: azure/webapps-deploy@v3
  with:
    app-name: 'employee-dashboard-{your-actual-name}'
    publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
    package: ./publish
```

### Step 6: Push & Deploy
```bash
git add .github/workflows/deploy.yml
git commit -m "Configure Azure"
git push origin master
```

**Your URL:** `https://employee-dashboard-{id}.azurewebsites.net`

---

## 🎯 Which Should You Choose?

| Platform | Difficulty | Free Tier | Best For |
|----------|-----------|-----------|----------|
| **Render** | ⭐ Easy | ✅ Yes | Beginners, Quick demos |
| **Azure** | ⭐⭐ Medium | ✅ Yes | Production, .NET apps |
| **Docker** | ⭐⭐⭐ Advanced | Depends | Flexibility |

**Recommendation:** Start with **Render** for quick deployment, then move to **Azure** for production.

---

## ✅ Test Your Deployment

Once deployed, test:
1. ✅ App loads: `https://your-url.com`
2. ✅ Login works: `admin@company.com` / `Admin@123`
3. ✅ Dashboard shows data
4. ✅ Can create/edit employees

---

**Need detailed steps?** See `DEPLOYMENT_GUIDE.md` for complete instructions.
