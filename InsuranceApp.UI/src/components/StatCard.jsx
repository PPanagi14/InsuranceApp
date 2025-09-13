import { Paper, Typography, Box } from "@mui/material";

export function StatCard({ title, value, icon, subtitle }) {
  return (
    <Paper sx={{ p: 3 }}>
      {/* Top row with title left, icon right */}
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Typography variant="overline" color="text.secondary">
          {title}
        </Typography>
        {icon}
      </Box>

      {/* Value and subtitle */}
      <Box display="flex" alignItems="baseline" gap={1} mt={1}>
        <Typography variant="h4">{value}</Typography>
        {subtitle ? (
          <Typography variant="body2" color="text.secondary">
            ({subtitle})
          </Typography>
        ) : null}
      </Box>
    </Paper>
  );
}
