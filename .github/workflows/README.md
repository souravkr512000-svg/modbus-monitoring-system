# GitHub Actions CI/CD Workflows

This repository includes GitHub Actions workflows for continuous integration and deployment of the EmployeeDashboard application.

## 📋 Available Workflows

### 1. CI/CD Pipeline (`ci-cd.yml`)
**Triggers:**
- Push to `master` or `main` branch
- Pull requests to `master` or `main` branch
- Manual trigger via workflow_dispatch

**Jobs:**
- **Build and Test** (Ubuntu): Builds, tests, and publishes the application
- **Build on Windows**: Ensures Windows compatibility
- **Build on macOS**: Ensures macOS compatibility
- **Code Quality**: Runs code quality checks and formatting verification

**Artifacts:**
- Published application files are uploaded as artifacts (retained for 7 days)

### 2. Deploy to Production (`deploy.yml`)
**Triggers:**
- Push to `master` or `main` branch
- Version tags (e.g., `v1.0.0`)
- Manual trigger with environment selection

**Features:**
- Builds and publishes the application
- Ready for deployment to various hosting providers
- Supports multiple environments (production/staging)

## 🚀 Setup Instructions

### Basic Setup (Already Configured)
The workflows are pre-configured and will run automatically on push/PR. No additional setup needed!

### Viewing Workflow Runs
1. Go to your GitHub repository
2. Click on the **"Actions"** tab
3. See all workflow runs and their status

### Manual Trigger
1. Go to **Actions** tab
2. Select the workflow you want to run
3. Click **"Run workflow"**
4. Select branch and options
5. Click **"Run workflow"**

## 🔧 Configuring Deployment

### Option 1: Azure App Service
1. Create an Azure App Service
2. Download the publish profile
3. Add secret to GitHub:
   - Go to **Settings** → **Secrets and variables** → **Actions**
   - Add secret: `AZURE_PUBLISH_PROFILE` (paste publish profile content)
4. Uncomment Azure deployment steps in `deploy.yml`
5. Update `app-name` with your Azure app name

### Option 2: Render
1. Create a Render service
2. Get your service ID and deploy key from Render dashboard
3. Add secrets to GitHub:
   - `RENDER_SERVICE_ID`: Your Render service ID
   - `RENDER_DEPLOY_KEY`: Your Render deploy key
4. Uncomment Render deployment steps in `deploy.yml`

### Option 3: Docker Hub
1. Create a Docker Hub account
2. Add secrets to GitHub:
   - `DOCKER_USERNAME`: Your Docker Hub username
   - `DOCKER_PASSWORD`: Your Docker Hub password/token
3. Create a `Dockerfile` in the repository root
4. Uncomment Docker deployment steps in `deploy.yml`
5. Update image name with your Docker Hub username

### Option 4: Other Providers
You can add custom deployment steps in `deploy.yml` for:
- AWS Elastic Beanstalk
- Google Cloud Run
- Heroku
- Any other hosting provider

## 📊 Workflow Status Badge

Add this to your README.md to show build status:

```markdown
![CI/CD](https://github.com/souravkr512000-svg/modbus-monitoring-system/workflows/CI/CD%20Pipeline/badge.svg)
```

## 🔍 Monitoring Workflows

### View Logs
- Click on any workflow run
- Click on a job to see detailed logs
- Expand individual steps to see output

### Common Issues

**Build Fails:**
- Check .NET SDK version matches (8.0.x)
- Verify all dependencies are in `.csproj`
- Check for compilation errors in logs

**Tests Fail:**
- Tests are optional (continue-on-error: true)
- Add test projects if needed
- Check test output in logs

**Deployment Fails:**
- Verify secrets are set correctly
- Check hosting provider credentials
- Review deployment logs

## 🎯 Best Practices

1. **Branch Protection**: Protect `master`/`main` branch to require CI checks
2. **Secrets Management**: Never commit secrets; use GitHub Secrets
3. **Environment Variables**: Use GitHub Environments for different deployment targets
4. **Notifications**: Set up email/Slack notifications for failed builds
5. **Artifacts**: Download and test artifacts before deploying

## 📝 Customization

### Modify Build Configuration
Edit `ci-cd.yml`:
```yaml
- name: Build project
  run: dotnet build ${{ env.PROJECT_PATH }} --configuration Release --no-restore
```

### Add More Jobs
Add new jobs to workflows:
```yaml
new-job:
  name: Custom Job
  runs-on: ubuntu-latest
  steps:
    - name: Custom Step
      run: echo "Hello World"
```

### Change Triggers
Modify `on:` section:
```yaml
on:
  schedule:
    - cron: '0 0 * * *'  # Daily at midnight
  push:
    branches: [ develop ]
```

## 🆘 Troubleshooting

**Workflow not running?**
- Check if file is in `.github/workflows/` directory
- Verify YAML syntax is correct
- Check branch name matches trigger conditions

**Permission denied?**
- Ensure GitHub Actions is enabled in repository settings
- Check workflow file permissions

**Secrets not working?**
- Verify secret names match exactly (case-sensitive)
- Check if secrets are set in correct repository/environment

## 📚 Resources

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [.NET GitHub Actions](https://github.com/actions/setup-dotnet)
- [Workflow Syntax](https://docs.github.com/en/actions/reference/workflow-syntax-for-github-actions)

---

**Need help?** Check the workflow logs or GitHub Actions documentation.
