import { useState, useEffect } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import {
  Box,
  Paper,
  Typography,
  Button,
  CircularProgress,
  Chip,
  Divider
} from '@mui/material'
import ArrowBackIcon from '@mui/icons-material/ArrowBack'
import PersonIcon from '@mui/icons-material/Person'
import CalendarTodayIcon from '@mui/icons-material/CalendarToday'

function DocumentDetailPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const [document, setDocument] = useState(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    const fetchDocument = async () => {
      setLoading(true)
      setError(null)
      
      try {
        const response = await fetch(`/api/documents/${id}`)
        if (!response.ok) {
          throw new Error('Document not found')
        }
        const data = await response.json()
        setDocument(data)
      } catch (err) {
        setError(err.message)
      } finally {
        setLoading(false)
      }
    }

    fetchDocument()
  }, [id])

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" my={4}>
        <CircularProgress />
      </Box>
    )
  }

  if (error) {
    return (
      <Box>
        <Button
          startIcon={<ArrowBackIcon />}
          onClick={() => navigate('/')}
          sx={{ mb: 2 }}
        >
          Back to Search
        </Button>
        <Typography color="error" align="center">
          {error}
        </Typography>
      </Box>
    )
  }

  if (!document) {
    return null
  }

  const formattedDate = new Date(document.createdDate).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  })

  return (
    <Box>
      <Button
        startIcon={<ArrowBackIcon />}
        onClick={() => navigate('/')}
        sx={{ mb: 3 }}
      >
        Back to Search
      </Button>

      <Paper elevation={3} sx={{ p: 4 }}>
        <Typography variant="h3" component="h1" gutterBottom>
          {document.title}
        </Typography>

        <Box display="flex" gap={2} alignItems="center" mb={2}>
          <Box display="flex" alignItems="center" gap={0.5}>
            <PersonIcon fontSize="small" color="action" />
            <Typography variant="body2" color="text.secondary">
              {document.author}
            </Typography>
          </Box>
          <Box display="flex" alignItems="center" gap={0.5}>
            <CalendarTodayIcon fontSize="small" color="action" />
            <Typography variant="body2" color="text.secondary">
              {formattedDate}
            </Typography>
          </Box>
        </Box>

        {document.tags && document.tags.length > 0 && (
          <Box display="flex" gap={1} mb={3} flexWrap="wrap">
            {document.tags.map((tag, index) => (
              <Chip key={index} label={tag} size="small" color="primary" />
            ))}
          </Box>
        )}

        <Divider sx={{ my: 3 }} />

        <Typography variant="h6" gutterBottom>
          Description
        </Typography>
        <Typography variant="body1" paragraph color="text.secondary">
          {document.description}
        </Typography>

        <Divider sx={{ my: 3 }} />

        <Typography variant="h6" gutterBottom>
          Content
        </Typography>
        <Typography variant="body1" paragraph>
          {document.content}
        </Typography>
      </Paper>
    </Box>
  )
}

export default DocumentDetailPage
