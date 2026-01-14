import { Pie } from "react-chartjs-2"
import { Chart as ChartJS, ArcElement, Tooltip } from "chart.js"
import { generateHexColors } from "../utils/colorGenerator"
import type { IPieDataItem } from "../types/IPieDataItem.interface"

ChartJS.register(ArcElement, Tooltip)
/**
 * Props for the PieChart component.
 * 
 * @typedef {Object} PieChartProps
 * @property {IPieDataItem[]} data - Array of data objects to display in the pie chart.
 * Each object should have:
 *  - `label`: string representing the slice label.
 *  - `value`: number representing the slice value.
 */
interface PieChartProps {
    data: IPieDataItem[]
}

/**
 * PieChart component.
 *
 * This component renders a pie chart using Chart.js via the react-chartjs-2 wrapper.
 * The chart automatically generates distinct colors for each slice based on the
 * number of data items using `generateHexColors` function.
 * @component
 * @param {PieChartProps} props - Component props.
 * @returns {JSX.Element} The rendered Pie chart.
 */
export function PieChart({ data }: PieChartProps) {
    const chartData = {
        labels: data.map((d) => d.label),
        datasets: [
            {
                data: data.map((d) => d.value),
                backgroundColor: generateHexColors(data.length),
                borderWidth: 1,
            },
        ],
    }

    return (
        <div style={{ maxWidth: 400, margin: "0 auto" }}>
            <Pie data={chartData} />
        </div>
    )
}
