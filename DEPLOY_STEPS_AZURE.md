# ☁️ Step-by-Step: Deploy to Azure App Service

## 📸 Visual Step-by-Step Instructions

### STEP 1: Create Azure Account
```
1. Go to: https://azure.microsoft.com/free/
2. Click "Start free" button
3. Sign in with Microsoft account
4. Fill in:
   - Country/Region
   - Phone number (for verification)
   - Credit card (won't be charged for free tier)
5. Verify identity
6. ✅ Azure account created!
```

**What you'll see:**
- Azure Portal dashboard

---

### STEP 2: Create Web App Resource
```
1. In Azure Portal, click "Create a resource" (top left)
2. Search box: Type "Web App"
3. Click "Web App" from results
4. Click "Create" button
```

**What you'll see:**
- "Create Web App" form

---

### STEP 3: Fill Basic Settings

**Basics Tab:**

```
Subscription: [Select your subscription]
Resource Group: 
  → Click "Create new"
  → Name: employee-dashboard-rg
  → Click "OK"

Name: employee-dashboard-[your-unique-id]
  (Must be globally unique, try adding numbers/letters)

Publish: Code
Runtime stack: .NET 8
Operating System: Linux (recommended)
Region: [Choose closest to you, e.g., East US]
```

**What you'll see:**
- Form with all these fields

---

### STEP 4: Create App Service Plan
```
App Service Plan:
  → Click "Create new"
  → Name: employee-dashboard-plan
  → Operating System: Linux
  → Pricing tier: 
      → Click "Dev/Test" tab
      → Select "Free F1" (for testing)
      → OR "Basic B1" ($13/month) for production
  → Click "Apply"
```

**What you'll see:**
- App Service Plan details

---

### STEP 5: Review and Create
```
1. Click "Review + create" button (bottom)
2. Wait for validation (green checkmarks)
3. Click "Create" button
4. ⏳ Wait 2-3 minutes for deployment
```

**What you'll see:**
- "Deployment in progress..." notification
- Then "Your deployment is complete"

---

### STEP 6: Go to Your App Service
```
1. Click "Go to resource" button
2. OR search for your app name in top search bar
3. Click on your app service
```

**What you'll see:**
- Your App Service overview page

---

### STEP 7: Configure Deployment from GitHub
```
1. In left menu, click "Deployment Center"
2. Under "Source", you'll see options
3. Click "Settings" tab
4. Configure:
   Source: GitHub
   Organization: [Your GitHub username]
   Repository: modbus-monitoring-system
   Branch: master
   Build provider: GitHub Actions
5. Click "Save" button
```

**What you'll see:**
- GitHub authorization prompt
- After authorizing: "Deployment configured"

---

### STEP 8: Get Publish Profile
```
1. Still in App Service page
2. Click "Get publish profile" button (top toolbar)
3. File downloads: [app-name].PublishSettings
4. Open file in text editor (Notepad/VS Code)
5. Copy ALL the XML content
```

**What you'll see:**
- XML file with connection details

---

### STEP 9: Add GitHub Secret
```
1. Go to GitHub: https://github.com/souravkr512000-svg/modbus-monitoring-system
2. Click "Settings" tab (top menu)
3. Click "Secrets and variables" → "Actions"
4. Click "New repository secret"
5. Fill:
   Name: AZURE_PUBLISH_PROFILE
   Secret: [Paste the entire XML from publish profile]
6. Click "Add secret"
```

**What you'll see:**
- Secret added successfully message

---

### STEP 10: Update deploy.yml Workflow
```
1. In GitHub repo, go to: .github/workflows/deploy.yml
2. Click "Edit" (pencil icon)
3. Find this section (around line 30-35):
   
   # For Azure App Service:
   # - name: Deploy to Azure
   #   uses: azure/webapps-deploy@v3
   #   with:
   #     app-name: 'your-app-name'
   #     publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
   #     package: ./publish

4. Uncomment it (remove # symbols)
5. Replace 'your-app-name' with your actual Azure app name
   Example: 'employee-dashboard-abc123'
6. Click "Commit changes" → "Commit directly to master"
```

**What you'll see:**
- Updated workflow file

---

### STEP 11: Trigger Deployment
```
1. Go to "Actions" tab in GitHub
2. You'll see workflow running automatically
3. Click on the workflow run
4. Watch it build and deploy
5. ⏳ Wait 5-10 minutes
```

**What you'll see:**
- Workflow progress: "Build" → "Deploy" → "Complete"

---

### STEP 12: Access Your App
```
1. Go back to Azure Portal
2. Your App Service → Overview
3. Find "Default domain"
4. URL: https://employee-dashboard-[id].azurewebsites.net
5. Click the URL or copy-paste in browser
```

**What you'll see:**
- Your Employee Dashboard login page!

---

### STEP 13: Configure Application Settings
```
1. In Azure Portal → Your App Service
2. Left menu → "Configuration"
3. Click "Application settings" tab
4. Click "+ New application setting"
5. Add:
   Name: ASPNETCORE_ENVIRONMENT
   Value: Production
6. Click "Save" (top)
7. Click "Continue" to confirm
```

**What you'll see:**
- Application settings list with your new setting

---

### STEP 14: Test Your Deployment
```
1. Open your Azure URL in browser
2. You should see: Login page
3. Login with:
   Email: admin@company.com
   Password: Admin@123
4. ✅ Dashboard loads successfully!
```

**What you'll see:**
- Employee Dashboard with data

---

## 🎉 Success Checklist

- [ ] ✅ Azure account created
- [ ] ✅ Web App created
- [ ] ✅ GitHub connected
- [ ] ✅ Publish profile downloaded
- [ ] ✅ GitHub secret added
- [ ] ✅ Workflow updated
- [ ] ✅ Deployment completed
- [ ] ✅ App accessible via URL
- [ ] ✅ Login works
- [ ] ✅ Dashboard displays

---

## 🐛 Troubleshooting

### Problem: Can't find "Web App" in Azure
**Solution:**
- Search for "App Service" instead
- Or use direct link: https://portal.azure.com/#create/Microsoft.WebSite

### Problem: Name already taken
**Solution:**
- Add random numbers/letters: `employee-dashboard-abc123`
- Must be globally unique across all Azure

### Problem: GitHub Actions workflow fails
**Solution:**
1. Check workflow logs in GitHub Actions tab
2. Verify app name matches exactly
3. Verify secret name is: `AZURE_PUBLISH_PROFILE`
4. Check publish profile XML is complete

### Problem: App shows 500 error
**Solution:**
1. Azure Portal → App Service → "Log stream"
2. Check for errors
3. Verify `ASPNETCORE_ENVIRONMENT=Production` is set
4. Check "Diagnose and solve problems" in Azure Portal

### Problem: Can't access GitHub repo
**Solution:**
1. Azure Portal → Deployment Center → Settings
2. Click "Authorize" next to GitHub
3. Re-authenticate GitHub access

---

## 💰 Azure Pricing

**Free Tier (F1):**
- ✅ 1 GB storage
- ✅ 1 GB outbound data transfer
- ✅ Shared infrastructure
- ✅ Perfect for testing

**Basic Tier (B1) - $13/month:**
- ✅ Always on
- ✅ 10 GB storage
- ✅ Better performance
- ✅ Recommended for production

---

## 🔄 Updating Your App

**Automatic Updates:**
- When you push to `master` branch
- GitHub Actions automatically deploys
- Just: `git push origin master`

**Manual Deploy:**
1. Azure Portal → App Service
2. "Deployment Center" → "Sync"
3. Or trigger from GitHub Actions manually

---

## 📊 What Happens During Deployment?

```
1. GitHub Actions workflow triggers
2. Builds .NET application
3. Publishes to ./publish folder
4. Connects to Azure using publish profile
5. Uploads files to Azure App Service
6. Restarts app service
7. ✅ Your app is live!
```

---

## 🎯 Next Steps

1. ✅ App is deployed!
2. Set up custom domain (optional)
3. Configure SSL certificate (automatic)
4. Set up monitoring/alerts
5. Configure backups

---

## 📞 Need Help?

1. Azure Documentation: https://docs.microsoft.com/azure/app-service
2. Check App Service logs: Portal → Log stream
3. Check GitHub Actions logs: Actions tab
4. Azure Support: Available in portal

---

**Your EmployeeDashboard is now live on Azure! 🚀**

**URL:** `https://employee-dashboard-[id].azurewebsites.net`
