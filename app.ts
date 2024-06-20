import express, { Request, Response } from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';
import helmet from 'helmet';

const app = express();
const PORT = 3000;
const DB_FILE = 'db.json';

app.use(helmet({
  contentSecurityPolicy: {
    directives: {
      defaultSrc: ["'self'"],
      styleSrc: ["'self'", 'https://fonts.googleapis.com'],
      fontSrc: ["'self'", 'https://fonts.gstatic.com']
    }
  }
}));

app.use(bodyParser.json());

// Ping endpoint
app.get('/ping', (req: Request, res: Response) => {
  res.json({ success: true });
});

// Submit endpoint
app.post('/submit', (req: Request, res: Response) => {
    const { name, email, phone, github_link, stopwatch_time } = req.body;
  
    if (!name || !email || !phone || !github_link || !stopwatch_time) {
      return res.status(400).json({ message: 'Missing required fields' });
    }
  
    const newSubmission = {
      name,
      email,
      phone,
      github_link,
      stopwatch_time
    };
  
    // Read existing submissions from DB
    let submissions = [];
    if (fs.existsSync(DB_FILE)) {
      const data = fs.readFileSync(DB_FILE, 'utf8');
      submissions = JSON.parse(data);
    }
  
    // Add new submission
    submissions.push(newSubmission);
  
    // Save updated submissions to DB
    fs.writeFileSync(DB_FILE, JSON.stringify(submissions, null, 2));
  
    res.json({ success: true });
  });
  
  

// Read endpoint
app.get('/read', (req: Request, res: Response) => {
  const { index } = req.query;

  const idx = Number(index);
  if (isNaN(idx)) {
    return res.status(400).json({ message: 'Invalid index' });
  }

  // Read submissions from DB
  if (fs.existsSync(DB_FILE)) {
    const data = fs.readFileSync(DB_FILE, 'utf8');
    const submissions = JSON.parse(data);

    if (idx >= 0 && idx < submissions.length) {
      res.json(submissions[idx]);
    } else {
      res.status(404).json({ message: 'Submission not found' });
    }
  } else {
    res.status(404).json({ message: 'DB file not found' });
  }
});

app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
});



