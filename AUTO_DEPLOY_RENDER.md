# 🚀 Auto-Deploy to Render - Just Click & Approve!

Everything is configured! Follow these simple steps:

## ✅ Step 1: Sign Up (1 minute)
1. Go to: **https://render.com**
2. Click **"Get Started for Free"**
3. Click **"Sign up with GitHub"** (one-click signup)
4. Authorize Render

## ✅ Step 2: Deploy (2 clicks!)
1. In Render dashboard, click **"New +"** → **"Blueprint"**
2. Select your repository: **`modbus-monitoring-system`**
3. Render will detect `render.yaml` automatically
4. Click **"Apply"** or **"Create Blueprint"**
5. ✅ That's it! Deployment starts automatically!

## ⏳ Step 3: Wait (5-10 minutes)
- Watch the build logs
- Render builds your Docker image
- Deploys automatically
- You'll get a URL like: `https://employee-dashboard.onrender.com`

## ✅ Step 4: Test
1. Open your URL
2. Login with: `admin@company.com` / `Admin@123`
3. ✅ Done!

---

## 🎯 What's Already Configured?

✅ **Dockerfile** - Optimized for Render  
✅ **render.yaml** - Auto-configuration file  
✅ **Program.cs** - Handles PORT environment variable  
✅ **Environment variables** - Production settings  

**Everything is ready! Just connect your repo and deploy!**

---

## 📝 Alternative: Manual Setup (if Blueprint doesn't work)

If Blueprint doesn't work, use manual setup:

1. **New +** → **Web Service**
2. Connect repo: `modbus-monitoring-system`
3. Settings:
   - **Name**: `employee-dashboard`
   - **Environment**: `Docker`
   - **Dockerfile Path**: `EmployeeDashboard/Dockerfile`
   - **Docker Build Context**: `EmployeeDashboard`
4. **Environment Variables**:
   - `ASPNETCORE_ENVIRONMENT` = `Production`
   - `PORT` = `10000`
5. Click **"Create Web Service"**

---

**That's it! Your app will be live in minutes! 🎉**
