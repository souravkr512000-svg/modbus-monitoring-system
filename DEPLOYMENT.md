# Deployment Guide

## 🚀 GitHub Repository
**Repository URL:** https://github.com/souravkr512000-svg/modbus-monitoring-system

## 📦 Deployment Options

### Option 1: Vercel (Frontend) + Render (Backend) - Recommended

#### Deploy Backend to Render:
1. Go to https://render.com
2. Sign up/Login with GitHub
3. Click "New +" → "Web Service"
4. Connect your GitHub repository: `modbus-monitoring-system`
5. Configure:
   - **Name**: `modbus-monitoring-backend`
   - **Root Directory**: Leave empty (root)
   - **Environment**: `Node`
   - **Build Command**: `npm install`
   - **Start Command**: `npm start`
   - **Instance Type**: Free tier is fine
6. Add Environment Variable:
   - `PORT` = `10000` (Render sets this automatically)
7. Click "Create Web Service"
8. Wait for deployment (2-3 minutes)
9. Copy your backend URL (e.g., `https://modbus-monitoring-backend.onrender.com`)

#### Deploy Frontend to Vercel:
1. Go to https://vercel.com
2. Sign up/Login with GitHub
3. Click "New Project"
4. Import repository: `modbus-monitoring-system`
5. Configure:
   - **Framework Preset**: Create React App
   - **Root Directory**: `client`
   - **Build Command**: `npm run build`
   - **Output Directory**: `build`
6. Add Environment Variable:
   - `REACT_APP_API_URL` = Your Render backend URL
7. Click "Deploy"
8. Your app will be live in 2-3 minutes!

**Frontend URL:** `https://modbus-monitoring-system.vercel.app`  
**Backend URL:** `https://modbus-monitoring-backend.onrender.com`

---

### Option 2: Railway (Full Stack) - Alternative

1. Go to https://railway.app
2. Sign up/Login with GitHub
3. Click "New Project" → "Deploy from GitHub repo"
4. Select `modbus-monitoring-system`
5. Railway will auto-detect Node.js
6. Add a second service for the frontend:
   - Click "+ New" → "GitHub Repo"
   - Same repo, but set:
     - **Root Directory**: `client`
     - **Build Command**: `npm run build`
     - **Start Command**: `npx serve -s build`
7. Set environment variables for frontend:
   - `REACT_APP_API_URL` = Backend service URL
8. Both services will be deployed!

---

### Option 3: Netlify (Frontend) + Railway (Backend)

#### Backend (Railway):
- Follow Railway steps from Option 2

#### Frontend (Netlify):
1. Go to https://netlify.com
2. Sign up/Login with GitHub
3. Click "Add new site" → "Import an existing project"
4. Select `modbus-monitoring-system`
5. Configure:
   - **Base directory**: `client`
   - **Build command**: `npm run build`
   - **Publish directory**: `client/build`
6. Add Environment Variable:
   - `REACT_APP_API_URL` = Your Railway backend URL
7. Deploy!

---

## 🔧 Update Frontend for Production

After deploying backend, update `client/src/App.js`:

```javascript
// Change this line:
const socket = io('http://localhost:3001');

// To your deployed backend URL:
const socket = io(process.env.REACT_APP_API_URL || 'https://your-backend-url.com');
```

And update API calls:
```javascript
// Change:
axios.get('http://localhost:3001/api/devices')

// To:
axios.get(`${process.env.REACT_APP_API_URL || 'https://your-backend-url.com'}/api/devices`)
```

---

## ✅ Quick Deploy Script

I've created configuration files:
- `render.yaml` - For Render backend deployment
- `vercel.json` - For Vercel frontend deployment
- `client/vercel.json` - Alternative Vercel config

Just connect your GitHub repo to these platforms and they'll auto-deploy!

---

## 📝 Interview Demo URL

Once deployed, you'll have:
- **Live Demo URL**: Share this in your interview!
- **GitHub Repository**: Shows your code
- **Professional Presentation**: Demonstrates deployment skills

---

## 🎯 Recommended: Vercel + Render

**Why?**
- ✅ Free tier available
- ✅ Easy setup
- ✅ Auto-deploy on git push
- ✅ Professional URLs
- ✅ Good performance

**Steps:**
1. Deploy backend to Render (5 minutes)
2. Deploy frontend to Vercel (5 minutes)
3. Update frontend environment variable
4. Done! 🎉

---

Need help? Check the deployment logs in your platform's dashboard!
