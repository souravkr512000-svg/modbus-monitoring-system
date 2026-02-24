# 🔧 Troubleshooting Guide

## Issue: "Cannot GET /" or "Not Found" Error on Render

### Problem
When accessing `https://modbus-monitoring-system.onrender.com`, you see:
- "Cannot GET /" error, OR
- "Not Found" error

### Root Cause
The backend server is running but not serving the React frontend files. This happens because:
1. The React app needs to be built before deployment
2. The server needs to serve static files from the `client/build` folder
3. Render needs to build the React app during deployment

---

## ✅ Solution Steps

### Step 1: Update Your Render Deployment

1. **Go to Render Dashboard**: https://dashboard.render.com
2. **Select your service**: `modbus-monitoring-system`
3. **Go to Settings** → **Build & Deploy**
4. **Update Build Command** to:
   ```bash
   npm install && cd client && npm install && npm run build && cd ..
   ```
5. **Update Start Command** (should be):
   ```bash
   npm start
   ```
6. **Add Environment Variable**:
   - Key: `NODE_ENV`
   - Value: `production`
7. **Save Changes**
8. **Click "Manual Deploy"** → **"Deploy latest commit"**

### Step 2: Wait for Build to Complete

- Build will take 3-5 minutes
- Watch the logs to ensure:
  - ✅ `npm install` completes
  - ✅ `cd client && npm install` completes
  - ✅ `npm run build` completes (creates `client/build` folder)
  - ✅ Server starts successfully

### Step 3: Verify Deployment

After deployment completes:
1. Check the **Logs** tab for any errors
2. Visit your URL: `https://modbus-monitoring-system.onrender.com`
3. You should see the dashboard!

---

## 🔍 Common Issues & Fixes

### Issue 1: Build Fails - "npm: command not found"
**Fix**: Make sure Node.js version is set correctly in Render settings (Node 18+ recommended)

### Issue 2: Build Fails - "Cannot find module"
**Fix**: Check that all dependencies are in `package.json`. Run `npm install` locally to verify.

### Issue 3: "Cannot GET /" Still Appears
**Fix**: 
- Verify `NODE_ENV=production` is set
- Check logs to ensure `client/build` folder was created
- Verify server.js has static file serving code

### Issue 4: API Calls Fail (CORS or 404)
**Fix**: 
- Check that API routes start with `/api/`
- Verify Socket.IO connection uses the correct URL
- Check browser console for errors

### Issue 5: Socket.IO Connection Fails
**Fix**: 
- Render supports WebSockets, but verify the URL is correct
- Check that Socket.IO CORS is configured in server.js
- Verify the connection URL matches your Render URL

---

## 🧪 Test Locally First

Before deploying, test the production build locally:

```bash
# 1. Install all dependencies
npm install
cd client && npm install && cd ..

# 2. Build React app
cd client && npm run build && cd ..

# 3. Set production mode and start
set NODE_ENV=production
npm start

# 4. Open http://localhost:3001
# Should see the dashboard!
```

---

## 📋 Deployment Checklist

Before deploying to Render:

- [ ] Code is pushed to GitHub
- [ ] `server.js` has static file serving code
- [ ] `package.json` has correct build scripts
- [ ] `render.yaml` has correct build command
- [ ] `NODE_ENV=production` is set in Render
- [ ] Tested locally with production build

---

## 🚀 Alternative: Separate Frontend/Backend Deployment

If the combined deployment doesn't work, deploy separately:

### Backend Only (Render):
- Build Command: `npm install`
- Start Command: `npm start`
- Health Check: `/api/devices`

### Frontend (Vercel/Netlify):
- Root Directory: `client`
- Build Command: `npm run build`
- Output Directory: `build`
- Environment Variable: `REACT_APP_API_URL` = Your Render backend URL

Then update `client/src/App.js` to use the backend URL.

---

## 📞 Still Having Issues?

1. **Check Render Logs**: Look for error messages
2. **Check Build Output**: Verify `client/build` folder exists
3. **Test API Endpoints**: Try `https://your-app.onrender.com/api/devices`
4. **Check Browser Console**: Look for JavaScript errors

---

## ✅ Expected Behavior After Fix

When working correctly:
- ✅ Root URL (`/`) shows the React dashboard
- ✅ API endpoints (`/api/devices`) return JSON
- ✅ WebSocket connection works (real-time updates)
- ✅ No console errors in browser

---

**Last Updated**: After fixing server.js to serve static files
