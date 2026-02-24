# 🎯 Step-by-Step: Deploy to Render (Visual Guide)

## 📸 Visual Step-by-Step Instructions

### STEP 1: Create Render Account
```
1. Open browser → Go to: https://render.com
2. Click "Get Started for Free" (top right)
3. Click "Sign up with GitHub" (recommended)
4. Authorize Render to access GitHub
5. ✅ Account created!
```

**What you'll see:**
- Render dashboard with "New +" button

---

### STEP 2: Create New Web Service
```
1. Click "New +" button (top right)
2. Click "Web Service" from dropdown
3. You'll see: "Connect a repository"
```

**What you'll see:**
- List of your GitHub repositories

---

### STEP 3: Connect Your Repository
```
1. Find: "modbus-monitoring-system"
2. Click "Connect" button next to it
3. ✅ Repository connected!
```

**What you'll see:**
- Repository details page

---

### STEP 4: Configure Service Settings

**Fill in these fields:**

#### Basic Settings:
```
Name: employee-dashboard
Region: Oregon (US West) [or closest to you]
Branch: master
```

#### Build & Deploy Section:
**Option A: Using Docker (Recommended)**
```
Environment: Docker
Dockerfile Path: EmployeeDashboard/Dockerfile
Docker Build Context: EmployeeDashboard
```

**Option B: Using Native Build**
```
Environment: Native
Build Command: 
  cd EmployeeDashboard && dotnet restore && dotnet publish -c Release -o ./publish

Start Command:
  cd EmployeeDashboard && dotnet EmployeeDashboard.dll --urls http://0.0.0.0:$PORT
```

**What you'll see:**
- Form with all these fields

---

### STEP 5: Add Environment Variables
```
1. Scroll down to "Advanced" section
2. Click "Add Environment Variable" button
3. Add this:
   Key: ASPNETCORE_ENVIRONMENT
   Value: Production
4. Click "Add" or "Save"
```

**What you'll see:**
- Environment variables list with your new variable

---

### STEP 6: Choose Plan
```
1. Scroll to "Plan" section
2. Select: "Free" (for testing)
   OR "Starter" ($7/month) for production
3. Free plan includes:
   - 750 hours/month
   - Sleeps after 15 min inactivity
```

**What you'll see:**
- Plan selection (Free/Starter/Standard)

---

### STEP 7: Create and Deploy
```
1. Scroll to bottom
2. Click "Create Web Service" button (blue)
3. ⏳ Wait 5-10 minutes...
```

**What you'll see:**
- Build logs streaming in real-time
- Progress: "Building..." → "Deploying..." → "Live"

---

### STEP 8: Get Your URL
```
1. Once deployment completes
2. Look at top of page
3. You'll see: "Your service is live at:"
4. URL: https://employee-dashboard.onrender.com
5. Click the URL to open!
```

**What you'll see:**
- Green "Live" status
- Your unique URL

---

### STEP 9: Test Your Application
```
1. Open the URL in browser
2. You should see: Login page
3. Login with:
   Email: admin@company.com
   Password: Admin@123
4. ✅ Success! Dashboard loads
```

**What you'll see:**
- Employee Dashboard login page
- After login: Dashboard with employee data

---

## 🎉 Success Checklist

After deployment, verify:

- [ ] ✅ Service shows "Live" status
- [ ] ✅ URL opens without errors
- [ ] ✅ Login page displays
- [ ] ✅ Can login successfully
- [ ] ✅ Dashboard shows data
- [ ] ✅ No errors in logs

---

## 🐛 Troubleshooting

### Problem: Build Failed
**Solution:**
1. Click "Logs" tab
2. Look for error messages
3. Common issues:
   - Wrong Dockerfile path → Check it's `EmployeeDashboard/Dockerfile`
   - Missing dependencies → Check `.csproj` file
   - .NET version mismatch → Ensure using .NET 8.0

### Problem: App Shows Error Page
**Solution:**
1. Check "Logs" tab for errors
2. Verify environment variable: `ASPNETCORE_ENVIRONMENT=Production`
3. Check if port is set correctly (should use `$PORT`)

### Problem: Service Keeps Sleeping
**Solution:**
- Free tier sleeps after 15 min inactivity
- Upgrade to Starter plan ($7/month) for always-on
- Or use a "ping" service to keep it awake

### Problem: Can't Find Repository
**Solution:**
1. Go to Render dashboard
2. Click "New +" → "Web Service"
3. Click "Configure account" next to GitHub
4. Re-authorize GitHub access

---

## 📊 What Happens During Deployment?

```
1. Render clones your GitHub repo
2. Builds Docker image (or runs build command)
3. Installs .NET SDK
4. Restores NuGet packages
5. Builds your application
6. Publishes to production
7. Starts web server
8. ✅ Your app is live!
```

**Time:** Usually 5-10 minutes for first deployment

---

## 🔄 Updating Your App

**Automatic Updates:**
- Render auto-deploys when you push to `master` branch
- Just push to GitHub: `git push origin master`
- Render will rebuild and redeploy automatically!

**Manual Deploy:**
1. Go to Render dashboard
2. Click your service
3. Click "Manual Deploy" → "Deploy latest commit"

---

## 💰 Pricing

**Free Tier:**
- ✅ 750 hours/month
- ✅ Sleeps after 15 min inactivity
- ✅ Perfect for demos and testing

**Starter Plan ($7/month):**
- ✅ Always on (no sleeping)
- ✅ Better performance
- ✅ Recommended for production

---

## 🎯 Next Steps

1. ✅ Your app is deployed!
2. Share your URL with others
3. Add custom domain (optional)
4. Set up monitoring (optional)
5. Configure backups (optional)

---

## 📞 Need Help?

1. Check Render docs: https://render.com/docs
2. Check your build logs in Render dashboard
3. Verify your code builds locally first
4. Test with: `dotnet build` and `dotnet run`

---

**Your EmployeeDashboard is now live on Render! 🚀**

**URL:** `https://employee-dashboard.onrender.com`
